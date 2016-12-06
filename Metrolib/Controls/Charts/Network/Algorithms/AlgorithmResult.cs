// ReSharper disable CheckNamespace

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The result of a <see cref="INodeLayoutAlgorithm" /> that is eventually used to render
	///     the <see cref="NetworkViewNodeItem" />s.
	/// </summary>
	public sealed class AlgorithmResult
		: IReadOnlyList<NodePosition>
	{
		/// <summary>
		///     An empty result.
		/// </summary>
		public static AlgorithmResult Empty;

		private readonly NodePosition[] _values;

		static AlgorithmResult()
		{
			Empty = new AlgorithmResult(new NodePosition[0]);
		}

		private AlgorithmResult(NodePosition[] toArray)
		{
			if (toArray == null)
				throw new ArgumentNullException("toArray");

			_values = toArray;
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0} Nodes", _values.Length);
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<NodePosition> GetEnumerator()
		{
			return ((IEnumerable<NodePosition>) _values).GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		///     Creates a new result from the given positions.
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		public static AlgorithmResult Create(IEnumerable<NodePosition> values)
		{
			return new AlgorithmResult(values.ToArray());
		}

		/// <summary>
		/// Gets the number of elements in the collection.
		/// </summary>
		public int Count
		{
			get { return _values.Length; }
		}

		/// <summary>
		/// Gets the element at the specified index in the read-only list.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public NodePosition this[int index]
		{
			get { return _values[index]; }
		}
	}
}