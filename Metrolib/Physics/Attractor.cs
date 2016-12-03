using System;
using System.Windows;

namespace Metrolib.Physics
{
	/// <summary>
	///     Responsible for calculating and applying the force two bodies enacts upon each other.
	/// </summary>
	/// <remarks>
	///     A positive force value causes bodies to attract one another, a negative one
	///     causes the opposite.
	/// </remarks>
	public sealed class Attractor
	{
		/// <summary>
		///     The force this attractor enacts upon any body.
		/// </summary>
		public double Force;

		/// <summary>
		///     Initializes this attractor.
		/// </summary>
		/// <param name="force"></param>
		public Attractor(double force = 1)
		{
			Force = force;
		}

		/// <summary>
		///     Calculates and applies the forces the two given bodies enact upon each
		///     other, if they were to attrach each other <see cref="Force" />.
		/// </summary>
		/// <param name="b0"></param>
		/// <param name="b1"></param>
		public void ApplyForces(Body b0, Body b1)
		{
			Vector d = b1.Position - b0.Position;
			double distance = d.Length;
			if (Math.Abs(distance) > 0)
			{
				Vector force = Force / (distance) * d / distance;

				b0.Force += force;
				b1.Force -= force;
			}
		}
	}
}