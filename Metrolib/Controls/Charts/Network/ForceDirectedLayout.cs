using Metrolib.Controls.Charts.Network.Algorithms;

namespace Metrolib.Controls.Charts.Network
{
	/// <summary>
	///     Defines the parameters for the "force directed layout" algorithm.
	///     Runs a physical simulation of the graph where nodes repulse each other
	///     and edges act as springs.
	/// </summary>
	/// <remarks>
	///     This layout is used by default if none other has been defined.
	/// </remarks>
	public sealed class ForceDirectedLayout
		: Layout
	{
		private double _l;
		private double _r;

		/// <summary>
		///     Initializes this layout.
		/// </summary>
		public ForceDirectedLayout()
		{
			_r = 0.001;
			_l = 100;
		}

		/// <summary>
		///     The amount of repulsive force two nodes enact on each other.
		/// </summary>
		public double R
		{
			get { return _r; }
			set
			{
				if (value == _r)
					return;

				_r = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The rest length of an edge between two nodes in device independent units.
		/// </summary>
		public double L
		{
			get { return _l; }
			set
			{
				if (value == _l)
					return;

				_l = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     Creates a new algorithm that realizes the layout described by this class.
		/// </summary>
		public override INodeLayoutAlgorithm CreateAlgorithm()
		{
			return new ForceDirectedLayoutAlgorithm(this);
		}
	}
}