using System;
using Metrolib.Geography;

// ReSharper disable CheckNamespace

namespace GeoVis
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Represents an axis aligned rectangle in mercator space.
	/// </summary>
	public struct MercatorRectangle : IEquatable<MercatorRectangle>
	{
		/// <summary>
		///     The greatest rectangle which can be displayed with this map.
		/// </summary>
		public static readonly MercatorRectangle Earth;

		/// <summary>
		/// </summary>
		public MercatorLocation Max;

		/// <summary>
		/// </summary>
		public MercatorLocation Min;

		static MercatorRectangle()
		{
			Earth = new MercatorRectangle
			{
				Min = MercatorLocation.Min,
				Max = MercatorLocation.Max
			};
		}

		/// <summary>
		///     Width of this rectangle.
		/// </summary>
		public double Width => Max.X - Min.X;

		/// <summary>
		///     Height of this rectangle.
		/// </summary>
		public double Height => Max.Y - Min.Y;

		/// <summary>
		///     Tests if this rectangle equals the given one.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(MercatorRectangle other)
		{
			return Min.Equals(other.Min) && Max.Equals(other.Max);
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("Min: {0}, Max: {1}", Min, Max);
		}

		/// <summary>
		///     Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(objA: null, objB: obj)) return false;
			return obj is MercatorRectangle && Equals((MercatorRectangle) obj);
		}

		/// <summary>
		///     Returns the hash code for this instance.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (Min.GetHashCode() * 397) ^ Max.GetHashCode();
			}
		}

		/// <summary>
		///     Tests the two given rectangles for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(MercatorRectangle left, MercatorRectangle right)
		{
			return left.Equals(right);
		}

		/// <summary>
		///     Tests the two given rectangles for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(MercatorRectangle left, MercatorRectangle right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		///     Creates a new rectangle from the given values.
		/// </summary>
		/// <param name="minX"></param>
		/// <param name="maxY"></param>
		/// <param name="maxX"></param>
		/// <param name="minY"></param>
		/// <returns></returns>
		public static MercatorRectangle FromMinMax(double minX, double maxY, double maxX, double minY)
		{
			return new MercatorRectangle
			{
				Min = new MercatorLocation(minX, minY),
				Max = new MercatorLocation(maxX, maxY)
			};
		}
	}
}