using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Metrolib
{
	/// <summary>
	/// </summary>
	/// <remarks>
	///     TODO: Move to separate project (maybe BCL extensions or something like that).
	/// </remarks>
	public static class ReadOnlyListExtensions
	{
		/// <summary>
		///     Creates a new slice onto this list that represents the portion identified
		///     by the given index and count.
		/// </summary>
		/// <remarks>
		///     DOES NOT PERFORM ANY COPY. MODIFICATIONS TO THIS LIST AFTER SLICE WILL HAVE SIDE
		///     EFFECTS FOR THE CREATED SLICES.
		/// </remarks>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="startIndex"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		[Pure]
		public static IReadOnlyList<T> Slice<T>(this IReadOnlyList<T> that, int startIndex, int count)
		{
			if (that == null)
				throw new NullReferenceException();

			if (startIndex < 0)
				throw new ArgumentOutOfRangeException(nameof(startIndex));

			if (count < 0)
				throw new ArgumentOutOfRangeException(nameof(count));

			if (startIndex + count > that.Count)
				throw new ArgumentOutOfRangeException();

			if (startIndex == 0 && count == that.Count)
				return that;

			return new ListSlice<T>(that, startIndex, count);
		}

		/// <summary>
		///     Creates a new slice onto this list that represents the portion of this list
		///     from the given index until the end.
		/// </summary>
		/// <remarks>
		///     DOES NOT PERFORM ANY COPY. MODIFICATIONS TO THIS LIST AFTER SLICE WILL HAVE SIDE
		///     EFFECTS FOR THE CREATED SLICES.
		/// </remarks>
		/// <typeparam name="T"></typeparam>
		/// <param name="that"></param>
		/// <param name="startIndex"></param>
		/// <returns></returns>
		[Pure]
		public static IReadOnlyList<T> Slice<T>(this IReadOnlyList<T> that, int startIndex)
		{
			return new ListSlice<T>(that, startIndex, that.Count - startIndex);
		}
	}
}