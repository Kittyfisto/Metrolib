using System;
using System.Reflection;
using System.Windows.Input;
using log4net;

namespace Metrolib
{
	public class DelegateCommand : ICommand
	{
		private readonly Action _execute;
		private readonly Func<bool> _canExecute;

		public DelegateCommand(Action execute)
		{
			if (execute == null) throw new ArgumentNullException("execute");

			_execute = execute;
		}

		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null) throw new ArgumentNullException("execute");
			if (canExecute == null) throw new ArgumentNullException("canExecute");

			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute();
		}

		public void Execute(object parameter)
		{
			_execute();
		}

		/// <summary>
		/// Fires the <see cref="CanExecuteChanged"/> event.
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			var fn = CanExecuteChanged;
			if (fn != null)
				fn(this, null);
		}

		/// <summary>
		/// Shall be fired whenever the command's <see cref="CanExecute"/> potentially returns a different
		/// value than previously.
		/// </summary>
		public event EventHandler CanExecuteChanged;
	}


	public class DelegateCommand<T> : ICommand where T : class
	{
		private static readonly ILog Log =
			LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly Action<T> _execute;
		private readonly Func<T, bool> _canExecute;

		public DelegateCommand(Action<T> execute)
		{
			if (execute == null) throw new ArgumentNullException("execute");

			_execute = execute;
		}

		public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			if (execute == null) throw new ArgumentNullException("execute");
			if (canExecute == null) throw new ArgumentNullException("canExecute");

			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute(GetParameter(parameter));
		}

		public void Execute(object parameter)
		{
			_execute(GetParameter(parameter));
		}

		private T GetParameter(object parameter)
		{
			var castedParameter = parameter as T;
			if (parameter != castedParameter)
			{
				Log.WarnFormat("Unable to cast '{0}' to {1}", parameter, typeof (T).FullName);
			}
			return castedParameter;
		}

		/// <summary>
		/// Fires the <see cref="CanExecuteChanged"/> event.
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			var fn = CanExecuteChanged;
			if (fn != null)
				fn(this, null);
		}

		/// <summary>
		/// Shall be fired whenever the command's <see cref="CanExecute"/> potentially returns a different
		/// value than previously.
		/// </summary>
		public event EventHandler CanExecuteChanged;
	}
}