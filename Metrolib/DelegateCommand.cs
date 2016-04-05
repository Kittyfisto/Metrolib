﻿using System;
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

#pragma warning disable 67
		public event EventHandler CanExecuteChanged;
#pragma warning restore 67
	}
}