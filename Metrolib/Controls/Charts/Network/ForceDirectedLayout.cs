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
		private double _distance;
		private double _springDampening;
		private double _springStiffness;
		private double _repulsiveness;

		/// <summary>
		///     Initializes this layout.
		/// </summary>
		public ForceDirectedLayout()
		{
			_repulsiveness = 1000;
			_springStiffness = 5;
			_springDampening = 2;
			_distance = 100;
		}

		/// <summary>
		///     The amount of repulsion that each pair of nodes enacts upon each other.
		/// </summary>
		/// <remarks>
		///     The greater this values, the faster nodes will move apart from each other.
		/// </remarks>
		/// <remarks>
		///     Should have the same magnitude (more or less) than <see cref="SpringStiffness" />, otherwise
		///     nodes may fly apart.
		/// </remarks>
		public double Repulsiveness
		{
			get { return _repulsiveness; }
			set
			{
				if (value == _repulsiveness)
					return;

				_repulsiveness = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The stiffness of the spring (aka spring constant) between two connected nodes.
		/// </summary>
		/// <remarks>
		///     The greater this value, the faster nodes will move due to being connected.
		/// </remarks>
		public double SpringStiffness
		{
			get { return _springStiffness; }
			set
			{
				if (value == _springStiffness)
					return;

				_springStiffness = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The dampening of the spring.
		/// </summary>
		/// <remarks>
		///     The greater this value, the more two connected nodes will decrease their velocity.
		/// </remarks>
		public double SpringDampening
		{
			get { return _springDampening; }
			set
			{
				if (value == _springDampening)
					return;

				_springDampening = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The desired distance between two connected nodes.
		/// </summary>
		/// <remarks>
		///     This property controls the spacing of the graph: The greater its value, the
		///     more nodes are apart from each other, in general.
		/// </remarks>
		/// <remarks>
		///     This is obviously only a suggestion to the algorithm. Depending on the number of nodes
		///     and their connecity, nodes can end up much closer or much farther away.
		/// </remarks>
		public double Distance
		{
			get { return _distance; }
			set
			{
				if (value == _distance)
					return;

				_distance = value;
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