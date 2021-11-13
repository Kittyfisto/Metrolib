using System;
using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Interface for the algorithm that actually determines the positions of nodes in the graph.
	/// </summary>
	/// <remarks>
	///     An algorithm is required to present a <see cref="Result" /> that specifies the positions for each node.
	///     Nodes without an assigned position are not presented by the view.
	/// </remarks>
	/// <remarks>
	///     An algorithm cannot determine the layout of edges. If this is desired, open an issue on github ;)
	/// </remarks>
	public interface INodeLayoutAlgorithm
		: IDisposable
	{
		/// <summary>
		///     The current result of the algorithm.
		/// </summary>
		AlgorithmResult Result { get; }

		/// <summary>
		///     Updates the algorithm.
		/// </summary>
		/// <remarks>
		///     Is *always* invoked from the UI thread.
		///     This method should not block for longer than a few milliseconds or otherwise the UI might become stuck.
		/// </remarks>
		/// <param name="elapsed"></param>
		void Update(TimeSpan elapsed);

		/// <summary>
		///     Adds the given node to the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		void AddNode(INode node);

		/// <summary>
		///     Removes the given node from the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		void RemoveNode(INode node);

		/// <summary>
		///     Removes all nodes from the graph.
		/// </summary>
		void ClearNodes();

		/// <summary>
		///     Freezes the given so that its position doesn't change until the node is unfrozen (<see cref="Unfreeze" />) again.
		/// </summary>
		/// <remarks>
		///     Is invoked by the view when the user starts dragging nodes around.
		/// </remarks>
		/// <param name="node"></param>
		void Freeze(INode node);

		/// <summary>
		///     Unfreezes the given node so that its may change, if the algorithm deems it necessary, of-course.
		/// </summary>
		/// <param name="node"></param>
		void Unfreeze(INode node);


		/// <summary>
		///     Adds the given edge to the graph.
		/// </summary>
		/// <remarks>
		///     Edges may point to nodes that have not been added (yet).
		/// </remarks>
		/// <param name="edge"></param>
		void AddEdge(IEdge edge);

		/// <summary>
		///     Removes the given list of edges from the graph.
		/// </summary>
		/// <remarks>
		///     Edges may point to nodes that have not been added (yet).
		/// </remarks>
		/// <param name="edge"></param>
		void RemoveEdge(IEdge edge);

		/// <summary>
		///     Removes all edges from the graph.
		/// </summary>
		void ClearEdges();

		/// <summary>
		/// Overwrites the position assigned by this algorithm with the given one.
		/// </summary>
		/// <param name="node"></param>
		/// <param name="position"></param>
		void SetPosition(INode node, Point position);
	}
}