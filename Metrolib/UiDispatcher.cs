using System;
using System.Threading;
using System.Windows.Threading;

namespace Metrolib
{
	/// <summary>
	///     Encapsulates a <see cref="Dispatcher" />.
	/// </summary>
	public sealed class UiDispatcher
		: IDispatcher
	{
		private readonly Dispatcher _dispatcher;

		/// <summary>
		///     Initializes this dispatcher.
		///     All calls to <see cref="BeginInvoke(Action)" /> will be forwarded to the given <paramref name="dispatcher" />.
		/// </summary>
		/// <param name="dispatcher"></param>
		public UiDispatcher(Dispatcher dispatcher)
		{
			_dispatcher = dispatcher;
		}

		public bool HasAccess
		{
			get { return Thread.CurrentThread == _dispatcher.Thread; }
		}

		/// <summary>
		///     Delays invocation of the given <paramref name="fn" /> until the dispatcher has time to execute it.
		/// </summary>
		/// <param name="fn"></param>
		public void BeginInvoke(Action fn)
		{
			BeginInvoke(fn, DispatcherPriority.Normal);
		}

		/// <summary>
		///     Delays invocation of the given <paramref name="fn" /> until the dispatcher has time to execute it.
		/// </summary>
		/// <param name="fn"></param>
		/// <param name="priority"></param>
		public void BeginInvoke(Action fn, DispatcherPriority priority)
		{
			_dispatcher.BeginInvoke(fn, priority);
		}
	}
}