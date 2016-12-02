using System.Collections.Generic;
using System.Windows;

namespace Metrolib.Physics
{
	/// <summary>
	///     The simplest of 'em all.
	/// </summary>
	public sealed class EulerIntegrator
	{
		private readonly double _forceDampening;
		private readonly double _velocityDampening;

		/// <summary>
		///     Initializes this integrator.
		/// </summary>
		/// <remarks>
		///     Both forces and velocities of all bodies are continously decreased so the simulation stays at rest eventually.
		/// </remarks>
		/// <param name="velocityDampening"></param>
		/// <param name="forceDampening"></param>
		public EulerIntegrator(double velocityDampening = 0.97, double forceDampening = 0.8)
		{
			_velocityDampening = velocityDampening;
			_forceDampening = forceDampening;
		}

		/// <summary>
		///     Updates the given bodies by the given amount of time.
		/// </summary>
		/// <param name="bodies"></param>
		/// <param name="dt"></param>
		public void Update(IEnumerable<Body> bodies, double dt)
		{
			foreach (Body body in bodies)
			{
				Update(body, dt);
			}
		}

		/// <summary>
		///     Updates the given body by the given amount of time.
		/// </summary>
		/// <param name="body"></param>
		/// <param name="dt"></param>
		public void Update(Body body, double dt)
		{
			Vector dv = body.Force*dt/body.Mass;
			body.Velocity += dv;
			Vector dp = body.Velocity*dt;
			body.Position += dp;

			body.Velocity *= _velocityDampening;
			body.Force *= _forceDampening;
		}
	}
}