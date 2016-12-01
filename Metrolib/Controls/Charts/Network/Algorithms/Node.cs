using System.Windows;

namespace Metrolib.Controls.Charts.Network.Algorithms
{
	/// <summary>
	///     A node as used by the internal layout algorithms.
	/// </summary>
	internal sealed class Node
	{
		public readonly object DataContext;
		public Vector Force;
		public Vector Velocity;
		public Point Position;

		public override string ToString()
		{
			return string.Format("{0}: {1}", DataContext, Position);
		}

		public Node(object dataContext)
		{
			DataContext = dataContext;
		}
	}
}