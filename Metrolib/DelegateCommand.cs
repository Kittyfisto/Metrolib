using System;
using System.Windows.Input;

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
}