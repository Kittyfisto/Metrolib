using System;
using System.Threading;
using System.Threading.Tasks;
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

		/// <inheritdoc />
		public bool HasAccess
		{
			get { return Thread.CurrentThread == _dispatcher.Thread; }
		}

		/// <inheritdoc />
		public void BeginInvoke(Action fn)
		{
			BeginInvoke(fn, DispatcherPriority.Normal);
		}

		/// <inheritdoc />
		public void BeginInvoke(Action fn, DispatcherPriority priority)
		{
			_dispatcher.BeginInvoke(fn, priority);
		}

		/// <inheritdoc />
		public Task BeginInvokeAsync(Action fn)
		{
			return BeginInvokeAsync(fn, DispatcherPriority.Normal);
		}

		/// <inheritdoc />
		public Task BeginInvokeAsync(Action fn, DispatcherPriority priority)
		{
			var completionSource = new TaskCompletionSource<int>();
			// TODO: What about cancelled? We should somehow keep track of cancelled actions that occur at application shutdown
			BeginInvoke(() =>
			{
				try
				{
					fn();
					completionSource.TrySetResult(42);
				}
				catch (Exception e)
				{
					completionSource.TrySetException(e);
				}
			}, priority);
			return completionSource.Task;
		}
	}
}