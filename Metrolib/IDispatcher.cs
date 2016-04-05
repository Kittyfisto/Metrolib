﻿using System;
using System.Windows.Threading;

namespace Metrolib
{
	public interface IDispatcher
	{
		void BeginInvoke(Action fn);
		void BeginInvoke(Action fn, DispatcherPriority priority);
	}
}