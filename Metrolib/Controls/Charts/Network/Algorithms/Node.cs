using Metrolib.Physics;

namespace Metrolib.Controls.Charts.Network.Algorithms
{
	/// <summary>
	///     A node as used by the internal layout algorithms.
	/// </summary>
	internal sealed class Node
	{
		/// <summary>
		///     The node in question.
		/// </summary>
		public readonly object DataContext;
		public readonly Body Body;

		public override string ToString()
		{
			return string.Format("{0}: {1}", DataContext, Body);
		}

		public Node(object dataContext)
		{
			DataContext = dataContext;
			Body = new Body();
		}
	}
}