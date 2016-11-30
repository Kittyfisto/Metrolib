using System.Windows;

namespace Metrolib.Controls.Charts.Network.Layout
{
	/// <summary>
	///     A node as used by the internal layout algorithms.
	/// </summary>
	internal sealed class Node
	{
		public readonly object DataContext;
		public Vector Force;
		public Point Position;

		public Node(object dataContext)
		{
			DataContext = dataContext;
		}
	}
}