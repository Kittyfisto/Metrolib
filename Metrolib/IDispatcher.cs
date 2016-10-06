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