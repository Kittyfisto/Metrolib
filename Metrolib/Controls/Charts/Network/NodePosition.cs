using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Defines the position of a node of the graph.
	/// </summary>
	public struct NodePosition
	{
		/// <summary>
		///     The node in question.
		/// </summary>
		public object Node;

		/// <summary>
		///     The position of that node.
		/// </summary>
		public Point Position;
	}
}