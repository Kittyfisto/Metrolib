using System;
using System.Diagnostics;
using System.Linq;

namespace Metrolib
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class ListSliceDebugView<T>
	{
		private readonly ListSlice<T> _slice;

		public ListSliceDebugView(ListSlice<T> slice)
		{
			if (slice == null)
				throw new ArgumentNullException(nameof(slice));

			_slice = slice;
		}

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items => _slice.ToArray();
	}
}