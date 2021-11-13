// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The direction of an edge.
	/// </summary>
	public enum EdgeDirection
	{
		/// <summary>
		///     The edge is not directed, i.e. is drawn without an arrow.
		/// </summary>
		Undirected,

		/// <summary>
		///     The edge is directed and points from <see cref="IEdge.Node1" /> TO <see cref="IEdge.Node2" />.
		///     Visually, this is represented by an arrow.
		/// </summary>
		Node1ToNode2,

		/// <summary>
		///     The edge is directed and points from <see cref="IEdge.Node2" /> TO <see cref="IEdge.Node1" />.
		///     Visually, this is represented by an arrow.
		/// </summary>
		Node2ToNode1,
	}
}