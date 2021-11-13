using System;
using System.Windows.Input;

namespace Metrolib
{
	/// <summary>
	///     An <see cref="ICommand" /> implementation that delegates both <see cref="Execute" /> and <see cref="CanExecute" />
	///     to user supplied delegates.
	/// </summary>
	public class DelegateCommand
		: ICommand
	{
		private readonly Func<bool> _canExecute;
		private readonly Action _execute;

		/// <summary>
		///     Initializes this delegate command.
		///     Since no <see cref="CanExecute" /> delegate is given, it is assumed that the command may always be executed.
		/// </summary>
		/// <param name="execute"></param>
		public DelegateCommand(Action execute)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));

			_execute = execute;
		}

		/// <summary>
		///     Initializes this delegate command.
		/// </summary>
		/// <param name="execute"></param>
		/// <param name="canExecute"></param>
		public DelegateCommand(Action execute, Func<bool> canExecute)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));
			if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));

			_execute = execute;
			_canExecute = canExecute;
		}

		/// <summary>
		///     Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute();
		}

		/// <summary>
		///     Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			_execute();
		}

		/// <summary>
		///     Shall be fired whenever the command's <see cref="CanExecute" /> potentially returns a different
		///     value than previously.
		/// </summary>
		public event EventHandler CanExecuteChanged;

		/// <summary>
		///     Fires the <see cref="CanExecuteChanged" /> event.
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, null);
		}
	}

	/// <summary>
	///     An <see cref="ICommand" /> implementation that delegates both <see cref="Execute" /> and <see cref="CanExecute" />
	///     to user supplied delegates.
	///     Furthermore, this implementation assumes that the target passed to both <see cref="Execute" /> and
	///     <see
	///         cref="CanExecute" />
	///     are of the given type <typeparamref name="T" /> and attempts to cast them to the given type, before forwarding them
	///     to the user supplied delegates.
	/// </summary>
	public class DelegateCommand<T> : ICommand where T : class
	{
		private readonly Func<T, bool> _canExecute;
		private readonly Action<T> _execute;

		/// <summary>
		///     Initializes this delegate command.
		///     Since no <see cref="CanExecute" /> delegate is given, it is assumed that the command may always be executed.
		/// </summary>
		/// <param name="execute"></param>
		public DelegateCommand(Action<T> execute)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));

			_execute = execute;
		}

		/// <summary>
		///     Initializes this delegate command.
		/// </summary>
		/// <param name="execute"></param>
		/// <param name="canExecute"></param>
		public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
		{
			if (execute == null) throw new ArgumentNullException(nameof(execute));
			if (canExecute == null) throw new ArgumentNullException(nameof(canExecute));

			_execute = execute;
			_canExecute = canExecute;
		}

		/// <summary>
		///     Defines the method that determines whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public bool CanExecute(object parameter)
		{
			return _canExecute == null || _canExecute(GetParameter(parameter));
		}

		/// <summary>
		///     Defines the method to be called when the command is invoked.
		/// </summary>
		/// <param name="parameter"></param>
		public void Execute(object parameter)
		{
			_execute(GetParameter(parameter));
		}

		/// <summary>
		///     Shall be fired whenever the command's <see cref="CanExecute" /> potentially returns a different
		///     value than previously.
		/// </summary>
		public event EventHandler CanExecuteChanged;

		private T GetParameter(object parameter)
		{
			var castedParameter = parameter as T;
			return castedParameter;
		}

		/// <summary>
		///     Fires the <see cref="CanExecuteChanged" /> event.
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, null);
		}
	}
}