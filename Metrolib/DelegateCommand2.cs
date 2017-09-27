using System;
using System.Windows.Input;

namespace Metrolib
{
	/// <summary>
	///     A simple <see cref="ICommand" /> implementation which forwards command invocations
	///     to the action given during construction. <see cref="CanBeExecuted" /> controls whether
	///     or not the command can be executed and is set to true by default.
	/// </summary>
	public sealed class DelegateCommand2
		: ICommand
	{
		private readonly Action _fn;
		private bool _canBeExecuted;

		/// <summary>
		///     Initializes this command: The given action will be invoked when this command is executed.
		/// </summary>
		/// <param name="fn"></param>
		public DelegateCommand2(Action fn)
		{
			if (fn == null)
				throw new ArgumentNullException(nameof(fn));

			_fn = fn;
			_canBeExecuted = true;
		}

		/// <summary>
		///     Whether or not this command can be executed.
		///     Set this property when this shall change.
		/// </summary>
		public bool CanBeExecuted
		{
			get { return _canBeExecuted; }
			set
			{
				if (value == _canBeExecuted)
					return;

				_canBeExecuted = value;
				EmitCanExecuteChanged();
			}
		}

		/// <inheritdoc />
		public bool CanExecute(object parameter)
		{
			return _canBeExecuted;
		}

		/// <inheritdoc />
		public void Execute(object parameter)
		{
			_fn();
		}

		/// <inheritdoc />
		public event EventHandler CanExecuteChanged;

		private void EmitCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}