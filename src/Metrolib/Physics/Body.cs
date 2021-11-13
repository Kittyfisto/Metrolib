using System.Windows;

namespace Metrolib.Physics
{
	/// <summary>
	///     Represents a body with all mass located in a single point.
	/// </summary>
	public sealed class Body
	{
		/// <summary>
		///     The force being applied to this body in this timestep.
		/// </summary>
		public Vector Force;

		/// <summary>
		///     The mass of this body.
		/// </summary>
		public double Mass;

		/// <summary>
		///     The position of this body.
		/// </summary>
		public Point Position;

		/// <summary>
		///     The current velocity of this body.
		/// </summary>
		public Vector Velocity;

		/// <summary>
		///     Initializes this body with a mass of 1.
		/// </summary>
		public Body()
		{
			Mass = 1;
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("Position {0}, Velocity {1}", Position, Velocity);
		}
	}
}