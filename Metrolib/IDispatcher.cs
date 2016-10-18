using System;
using System.Windows.Threading;

namespace Metrolib
{
	/// <summary>
	///     Interface for an object, capable of delaying the execution of user supplied actions to a later point in time.
	/// </summary>
	public interface IDispatcher
	{
		/// <summary>
		///     Whether or not the calling thread has access to the resource guarded by this dispatcher.
		/// </summary>
		/// <returns>
		///     Usually a dispatcher is used to guard access to a resource, for example a FrameworkElement.
		///     When a method doesn't have control over the thread it's called from (for example because it's
		///     an event delegate), then it might want to check if if can immediately access the resource,
		///     or if it should delegate the access via <see cref="BeginInvoke(Action)" />.
		/// </returns>
		/// <returns>True when the calling thread has access to the resource, otherwise false.</returns>
		bool HasAccess { get; }

		/// <summary>
		///     Adds the given action to the list of actions to be executed later on.
		/// </summary>
		/// <param name="fn"></param>
		void BeginInvoke(Action fn);

		/// <summary>
		///     Adds the given action to the list of actions to be executed later on.
		/// </summary>
		/// <param name="fn"></param>
		/// <param name="priority"></param>
		void BeginInvoke(Action fn, DispatcherPriority priority);
	}
}