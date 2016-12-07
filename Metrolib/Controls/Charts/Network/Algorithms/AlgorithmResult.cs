// ReSharper disable CheckNamespace

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The result of a <see cref="INodeLayoutAlgorithm" /> that is eventually used to render
	///     the <see cref="NetworkViewNodeItem" />s.
	/// </summary>
	public sealed class AlgorithmResult
		: IReadOnlyDictionary<INode, Point>
	{
		/// <summary>
		///     An empty result.
		/// </summary>
		public static AlgorithmResult Empty;

		private readonly IDictionary<INode, Point> _nodes;

		static AlgorithmResult()
		{
			Empty = new AlgorithmResult(new Dictionary<INode, Point>());
		}

		private AlgorithmResult(IDictionary<INode, Point> nodes)
		{
			_nodes = nodes;
		}

		/// <summary>
		///     Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator<KeyValuePair<INode, Point>> GetEnumerator()
		{
			return _nodes.GetEnumerator();
		}

		/// <summary>
		///     Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns></returns>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		///     Gets the number of elements in the collection.
		/// </summary>
		public int Count
		{
			get { return _nodes.Count; }
		}

		/// <summary>
		///     Determines whether the read-only dictionary contains an element that has the specified key.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool ContainsKey(INode key)
		{
			return _nodes.ContainsKey(key);
		}

		/// <summary>
		///     Gets the value that is associated with the specified key.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryGetValue(INode key, out Point value)
		{
			return _nodes.TryGetValue(key, out value);
		}

		/// <summary>
		///     Gets the element that has the specified key in the read-only dictionary.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public Point this[INode key]
		{
			get { return _nodes[key]; }
		}

		/// <summary>
		///     Gets an enumerable collection that contains the keys in the read-only dictionary.
		/// </summary>
		public IEnumerable<INode> Keys
		{
			get { return _nodes.Keys; }
		}

		/// <summary>
		///     Gets an enumerable collection that contains the values in the read-only dictionary.
		/// </summary>
		public IEnumerable<Point> Values
		{
			get { return _nodes.Values; }
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0} Nodes", _nodes.Count);
		}

		/// <summary>
		///     Creates a new result from the given positions.
		/// </summary>
		/// <param name="values"></param>
		/// <returns></returns>
		public static AlgorithmResult Create(IEnumerable<KeyValuePair<INode, Point>> values)
		{
			return new AlgorithmResult(values.ToDictionary(x => x.Key, x => x.Value));
		}
	}
}