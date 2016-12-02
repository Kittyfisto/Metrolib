using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib.Controls.Charts.Network.Algorithms
{
	public sealed class Spring
	{
		private readonly Random _rng;
		private readonly double _springConstant;
		private readonly double _springLength;
		private readonly double _dampening;

		public Spring(Random rng, double springConstant, double springLength, double dampening)
		{
			if (rng == null)
				throw new ArgumentNullException("rng");

			_rng = rng;
			_springConstant = springConstant;
			_springLength = springLength;
			_dampening = dampening;
		}

		[Pure]
		public Vector GetForce(Point p0, Point p1, Vector velocity)
		{
			var d = p1 - p0;
			var distance = d.Length;
			var displacement = (distance - _springLength);
			double force = -_springConstant*displacement;
			Vector attraction;
			if (Math.Abs(distance) > 0)
			{
				return _springConstant * (distance - _springLength) * d / distance - _dampening * velocity;
			}
			else
			{
				d = _rng.NextDirection();

				attraction = force*d;
			}

			var f = attraction ;
			return f;
		}
	}
}