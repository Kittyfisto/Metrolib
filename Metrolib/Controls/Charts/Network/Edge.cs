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
		public INode Node1 { get; set; }

		/// <summary>
		///     The second node of this edge.
		/// </summary>
		public INode Node2 { get; set; }

		/// <summary>
		///     Creates a new edge between the two given nodes.
		/// </summary>
		/// <param name="node1"></param>
		/// <param name="node2"></param>
		/// <returns></returns>
		public static Edge Create(INode node1, INode node2)
		{
			return new Edge {Node1 = node1, Node2 = node2};
		}
	}
}