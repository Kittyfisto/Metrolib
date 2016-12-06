using System;

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
	}
}