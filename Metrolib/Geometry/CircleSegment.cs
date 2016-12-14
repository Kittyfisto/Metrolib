using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib.Geometry
{
	/// <summary>
	///     A segment of a circle.
	/// </summary>
	public struct CircleSegment
	{
		/// <summary>
		///     The circle of which this segment represents a part of.
		/// </summary>
		public Circle Circle;

		/// <summary>
		/// </summary>
		public double EndAngle;

		/// <summary>
		/// </summary>
		public double StartAngle;

		/// <summary>
		///     The point at <see cref="StartAngle" /> on the circle.
		/// </summary>
		public Point StartPoint
		{
			get { return Circle.GetPoint(StartAngle); }
		}

		/// <summary>
		///     The point at <see cref="EndAngle" /> on the circle.
		/// </summary>
		public Point EndPoint
		{
			get { return Circle.GetPoint(EndAngle); }
		}

		/// <summary>
		///     The arc length of the circle segment.
		/// </summary>
		public double OpenAngle
		{
			get { return EndAngle - StartAngle; }
		}

		/// <summary>
		/// 
		/// </summary>
		public double Angle
		{
			get { return StartAngle + OpenAngle/2; }
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0}, Start: {1}, End: {2}", Circle, StartAngle, EndAngle);
		}

		/// <summary>
		///     Tests if the given point is inside this circle segment, or outside.
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		[Pure]
		public bool Contains(Point that)
		{
			double squaredDistance = (that - Circle.Center).LengthSquared;
			if (squaredDistance > Circle.Radius*Circle.Radius)
				return false;

			// see http://stackoverflow.com/questions/13640931/how-to-determine-if-a-vector-is-between-two-other-vectors
			// for explanation.

			Point p1 = Circle.GetPoint(StartAngle);
			Point p2 = Circle.GetPoint(EndAngle);

			Vector v0 = that - Circle.Center;
			Vector v1 = p1 - Circle.Center;
			Vector v2 = p2 - Circle.Center;

			double v1x0 = Vector.CrossProduct(v1, v0);
			double v1x2 = Vector.CrossProduct(v1, v2);
			double v2x1 = Vector.CrossProduct(v2, v1);
			double v2x0 = Vector.CrossProduct(v2, v0);

			return (v1x0*v1x2 >= 0) && (v2x1*v2x0 >= 0);
		}

		/// <summary>
		///     Tests if the given rectangle fully fits inside this circle segment.
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public bool Contains(Rect that)
		{
			bool tl = Contains(that.TopLeft);
			bool tr = Contains(that.TopRight);
			bool br = Contains(that.BottomRight);
			bool bl = Contains(that.BottomLeft);
			return tl && tr && br && bl;
		}
	}
}