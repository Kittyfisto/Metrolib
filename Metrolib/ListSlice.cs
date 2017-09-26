using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Metrolib
{
	/// <summary>
	///     Represents a slice onto a source collection.
	/// </summary>
	/// <remarks>
	///     TODO: Move to separate project (maybe BCL extensions or something like that).
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	[DebuggerTypeProxy(typeof(ListSliceDebugView<>)),
	 DebuggerDisplay("Count = {Count}")]
	public sealed class ListSlice<T>
		: IReadOnlyList<T>
	{
		private readonly IReadOnlyList<T> _source;
		private readonly int _startIndex;

		internal ListSlice(IReadOnlyList<T> source, int startIndex, int count)
		{
			_source = source;
			_startIndex = startIndex;
			Count = count;
		}

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			return new ListSliceEnumerator(_source, _startIndex, Count);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <inheritdoc />
		public int Count { get; }

		/// <inheritdoc />
		public T this[int index] => _source[_startIndex + index];

		private sealed class ListSliceEnumerator
			: IEnumerator<T>
		{
			private readonly int _count;
			private readonly IReadOnlyList<T> _source;
			private readonly int _startIndex;
			private int _currentIndex;

			public ListSliceEnumerator(IReadOnlyList<T> source, int startIndex, int count)
			{
				_source = source;
				_startIndex = startIndex;
				_count = count;
				Reset();
			}

			public void Dispose()
			{
			}

			public bool MoveNext()
			{
				++_currentIndex;
				if (_currentIndex >= _startIndex + _count)
					return false;

				return true;
			}

			public void Reset()
			{
				_currentIndex = _startIndex - 1;
			}

			public T Current => _source[_currentIndex];

			object IEnumerator.Current => Current;
		}
	}
}