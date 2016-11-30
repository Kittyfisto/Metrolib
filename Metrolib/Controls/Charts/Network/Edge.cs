// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Untyped edge between two nodes of type <see cref="object" /> in a graph.
	/// </summary>
	public sealed class Edge
		: IEdge
	{
		/// <summary>
		///     The direction the edge points in, if any.
		/// </summary>
		public EdgeDirection Direction { get; set; }

		/// <summary>
		///     The first node of this edge.
		/// </summary>
		public object Node1 { get; set; }

		/// <summary>
		///     The second node of this edge.
		/// </summary>
		public object Node2 { get; set; }

		/// <summary>
		/// Creates a new edge between the two given nodes.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <returns></returns>
		public static Edge<T> Create<T>(T node1, T node2)
		{
			return new Edge<T> {Node1 = node1, Node2 = node2};
		}
	}

	/// <summary>
	///     Edge between two nodes of type <typeparamref name="T" /> in a 2d graph.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class Edge<T>
		: IEdge
	{
		/// <summary>
		///     The direction the edge points in, if any.
		/// </summary>
		public EdgeDirection Direction { get; set; }

		/// <summary>
		///     The first node of this edge.
		/// </summary>
		public T Node1 { get; set; }

		/// <summary>
		///     The second node of this edge.
		/// </summary>
		public T Node2 { get; set; }

		object IEdge.Node1
		{
			get { return Node1; }
		}

		object IEdge.Node2
		{
			get { return Node2; }
		}
	}
}