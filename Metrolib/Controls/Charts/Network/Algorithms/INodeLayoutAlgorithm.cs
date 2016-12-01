using System;
using System.Collections.Generic;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Interface for the algorithm that actually determines the positions of nodes in the graph.
	/// </summary>
	public interface INodeLayoutAlgorithm
		: IDisposable
	{
		/// <summary>
		///     Updates the graph.
		/// </summary>
		/// <remarks>
		///     Is *always* invoked from the UI thread.
		///     This method should not block for longer than a few milliseconds or otherwise the UI might become stuck.
		/// </remarks>
		/// <param name="elapsed"></param>
		List<NodePosition> Update(TimeSpan elapsed);

		/// <summary>
		///     Adds the given node to the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		void AddNode(object node);

		/// <summary>
		///     Removes the given node from the list of nodes of the graph.
		/// </summary>
		/// <param name="node"></param>
		void RemoveNode(object node);

		/// <summary>
		///     Removes all nodes from the graph.
		/// </summary>
		void ClearNodes();

		/// <summary>
		///     Adds the given list of edges to the graph.
		/// </summary>
		/// <remarks>
		///     Edges may point to nodes that have not been added (yet).
		/// </remarks>
		/// <param name="edges"></param>
		void AddEdges(IEnumerable<IEdge> edges);

		/// <summary>
		///     Removes the given list of edges from the graph.
		/// </summary>
		/// <remarks>
		///     Edges may point to nodes that have not been added (yet).
		/// </remarks>
		/// <param name="edges"></param>
		void RemoveEdges(IEnumerable<IEdge> edges);

		/// <summary>
		///     Removes all edges from the graph.
		/// </summary>
		void ClearEdges();
	}
}