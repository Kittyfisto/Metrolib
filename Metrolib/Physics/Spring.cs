using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib.Physics
{
	/// <summary>
	///     Responsible for calculating the force a spring enacts upon its two end-points.
	///     <see cref="GetForce" />.
	/// </summary>
	public sealed class Spring
	{
		private readonly Random _rng;

		/// <summary>
		///     The dampening of the spring.
		///     Greater values mean greater dampening.
		/// </summary>
		public double Dampening;

		/// <summary>
		///     The rest-length of the spring.
		/// </summary>
		public double Length;

		/// <summary>
		///     The stiffness of the spring (spring constant).
		/// </summary>
		public double Stiffness;

		/// <summary>
		///     Initializes this spring with the default values of 1.
		/// </summary>
		public Spring(Random rng)
			: this(rng, 1, 1, 1)
		{
		}

		/// <summary>
		///     Initializes this spring with the given values.
		/// </summary>
		/// <param name="rng">Rng that is used to calculate the direction of force in case both end-points are coincident</param>
		/// <param name="stiffness"></param>
		/// <param name="length"></param>
		/// <param name="dampening"></param>
		public Spring(Random rng, double stiffness, double length, double dampening)
		{
			if (rng == null)
				throw new ArgumentNullException("rng");

			_rng = rng;
			Stiffness = stiffness;
			Length = length;
			Dampening = dampening;
		}

		/// <summary>
		///     Calculates the force that is enacted upon <paramref name="p0" /> and <paramref name="p1" /> (multiply the result by -1 for the latter).
		/// </summary>
		/// <param name="p0"></param>
		/// <param name="p1"></param>
		/// <param name="velocity"></param>
		/// <returns></returns>
		[Pure]
		public Vector GetForce(Point p0, Point p1, Vector velocity)
		{
			Vector d = p1 - p0;
			double distance = d.Length;
			double displacement = (distance - Length);
			double force = -Stiffness*displacement;
			Vector attraction;
			if (Math.Abs(distance) > 0)
			{
				return Stiffness*(distance - Length)*d/distance - Dampening*velocity;
			}
			else
			{
				d = _rng.NextDirection();

				attraction = force*d;
			}

			Vector f = attraction;
			return f;
		}

		/// <summary>
		///     Calculates and applies the forces enacted upon the two given bodies, if this spring were attached to both of them.
		/// </summary>
		/// <param name="b0"></param>
		/// <param name="b1"></param>
		/// <returns></returns>
		public void ApplyForces(Body b0, Body b1)
		{
			Vector velocity = b0.Velocity - b1.Velocity;
			Vector force = GetForce(b0.Position,
			                        b1.Position,
			                        velocity);
			b0.Force += force;
			b1.Force -= force;
		}
	}
}