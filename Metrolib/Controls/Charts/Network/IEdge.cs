// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Represents an edge in a 2-d graph.
	/// </summary>
	public interface IEdge
	{
		/// <summary>
		///     The direction the edge points in, if any.
		/// </summary>
		EdgeDirection Direction { get; }

		/// <summary>
		///     The first node of this edge.
		/// </summary>
		INode Node1 { get; }

		/// <summary>
		///     The second node of this edge.
		/// </summary>
		INode Node2 { get; }
	}
}