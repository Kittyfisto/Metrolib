using System;
using System.Windows.Input;

namespace Metrolib
{
	public class DelegateCommand : ICommand
	{
		private readonly Action _execute;

		public DelegateCommand(Action execute)
		{
			if (execute == null) throw new ArgumentNullException("execute");

			_execute = execute;
		}

		public bool CanExecute(object parameter)
		{
			return true;
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