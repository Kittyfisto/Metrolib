using System;
using Metrolib.Geometry;

namespace Metrolib.Geography
{
	/// <summary>
	///     Represents a point on earth in mercator space.
	/// </summary>
	public struct MercatorLocation
	{
		/// <summary>
		/// The scale parameter of the mercator projection.
		/// </summary>
		public static readonly double Scale;

		/// <summary>
		///     Minimum y-coordinate value.
		/// </summary>
		public static readonly double MinY;

		/// <summary>
		///     Maximum y-coordinate value.
		/// </summary>
		public static readonly double MaxY;

		/// <summary>
		///     Minimum x-coordinate value.
		/// </summary>
		public static readonly double MinX;

		/// <summary>
		///     Maximum x-coordinate value.
		/// </summary>
		public static readonly double MaxX;

		/// <summary>
		///     Width of mercator space.
		/// </summary>
		public static readonly double Width;

		/// <summary>
		///     Height of mercator space.
		/// </summary>
		public static readonly double Height;

		/// <summary>
		/// The point with the minimum x- and y-coordinate values.
		/// </summary>
		public static readonly MercatorLocation Min;

		/// <summary>
		/// The point with the maximum x- and y-coordinate values.
		/// </summary>
		public static readonly MercatorLocation Max;

		/// <summary>
		///     The center of mercator space (concident with <see cref="GeoLocation.Zero" />).
		/// </summary>
		public static readonly MercatorLocation Zero;

		private double _x;
		private double _y;

		static MercatorLocation()
		{
			Scale = 6378137;

			MinX = -Math.PI*Scale;
			MaxY = Math.PI*Scale;
			MaxX = Math.PI*Scale;
			MinY = -Math.PI*Scale;
			Width = MaxX - MinX;
			Height = MaxY - MinY;
			Min = new MercatorLocation {_x = MinX, _y = MinY};
			Max = new MercatorLocation {_x = MaxX, _y = MaxY};

			Zero = new MercatorLocation(0, 0);
		}

		/// <summary>
		///     Initializes a new
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		public MercatorLocation(double x, double y)
		{
			_x = x;
			_y = y;
		}

		/// <summary>
		///     The x-coordinate value of this location.
		/// </summary>
		public double X
		{
			get { return _x; }
			set { _x = value; }
		}

		/// <summary>
		///     The y-coordinate value of this location.
		/// </summary>
		public double Y
		{
			get { return _y; }
			set { _y = value; }
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("X: {0}, Y: {1}", X, Y);
		}

		/// <summary>
		///     Projects this point into WGS 84 space.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static explicit operator GeoLocation(MercatorLocation value)
		{
			// The y coordinate may never exceed the scene's rectangle
			value.Y = Math.Max(MinY, Math.Min(MaxY, value.Y));
			// The x coordinate is simply wrapped around the rectangle
			while (value.X > MaxX)
				value.X -= Width;
			while (value.X < MinX)
				value.X += Width;

			var ret = new GeoLocation
				{
					Longitude = Angle.ToDegrees(value.X/Scale),
					Latitude =
						Angle.ToDegrees(
							(2*Math.Atan(Math.Pow(Math.E, value.Y/Scale)) - Math.PI/2))
				};
			return ret;
		}

		/// <summary>
		///     Projects the given point into mercator space.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static explicit operator MercatorLocation(GeoLocation value)
		{
			if (value.Latitude > 90)
			{
				value.Latitude = 90;
			}
			else if (value.Latitude < -90)
			{
				value.Latitude = -90;
			}

			var point = new MercatorLocation();
			double sin = Math.Sin(Angle.ToRadians(value.Latitude));

			point.X = Angle.ToRadians(value.Longitude)*Scale;

			if (sin >= 1)
			{
				point.Y = MaxY;
			}
			else if (sin <= -1)
			{
				point.Y = MinY;
			}
			else
			{
				point.Y = (0.5*Math.Log((1 + sin)/(1 - sin)))*Scale;
				point.Y = Math.Min(Math.Max(point.Y, MinY), MaxY);
			}

			// Now we clamp those coordinates so they do not lie outside the valid range
			point.X = Math.Max(Math.Min(point.X, MaxX), MinX);

			return point;
		}
	}
}