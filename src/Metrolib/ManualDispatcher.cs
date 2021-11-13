using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Metrolib
{
	/// <summary>
	///     An <see cref="IDispatcher" /> implementation that only ever executes it's pending invocations
	///     when <see cref="InvokeAll" /> is called.
	/// </summary>
	/// <remarks>
	///     Particularly usefull for unit tests.
	/// </remarks>
	public sealed class ManualDispatcher
		: IDispatcher
	{
		private readonly SortedDictionary<DispatcherPriority, List<Action>> _pendingInvokes;

		/// <summary>
		///     Initializes this dispatcher.
		/// </summary>
		public ManualDispatcher()
		{
			_pendingInvokes = new SortedDictionary<DispatcherPriority, List<Action>>();
		}

		/// <summary>
		///     Whether or not the calling thread has access to the resource guarded by this dispatcher.
		/// </summary>
		public bool HasAccess
		{
			get { return false; }
		}

		/// <summary>
		///     Adds the given action to the list of actions to be executed later on.
		/// </summary>
		/// <param name="fn"></param>
		public void BeginInvoke(Action fn)
		{
			BeginInvoke(fn, DispatcherPriority.Normal);
		}

		/// <summary>
		///     Adds the given action to the list of actions to be executed later on.
		/// </summary>
		/// <param name="fn"></param>
		/// <param name="priority"></param>
		public void BeginInvoke(Action fn, DispatcherPriority priority)
		{
			lock (_pendingInvokes)
			{
				List<Action> invokes;
				if (!_pendingInvokes.TryGetValue(priority, out invokes))
				{
					invokes = new List<Action>();
					_pendingInvokes.Add(priority, invokes);
				}

				invokes.Add(fn);
			}
		}

		/// <summary>
		///     Executes all pending invocations and then clears the list of them.
		/// </summary>
		public void InvokeAll()
		{
			List<KeyValuePair<DispatcherPriority, List<Action>>> pendingInvokes;

			lock (_pendingInvokes)
			{
				pendingInvokes = _pendingInvokes.Select(x =>
				                                        new KeyValuePair<DispatcherPriority, List<Action>>(
					                                        x.Key, x.Value.ToList()
					                                        )).ToList();
				_pendingInvokes.Clear();
			}

			foreach (var pair in pendingInvokes)
			{
				List<Action> invokes = pair.Value;
				foreach (Action invoke in invokes)
				{
					invoke();
				}
			}
		}

		public Task BeginInvokeAsync(Action fn)
		{
			return BeginInvokeAsync(fn, DispatcherPriority.Normal);
		}

		public Task BeginInvokeAsync(Action fn, DispatcherPriority priority)
		{
			var completionSource = new TaskCompletionSource<int>();
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