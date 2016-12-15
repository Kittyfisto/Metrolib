using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib.Geometry
{
	/// <summary>
	///     A 2-d circle.
	/// </summary>
	public struct Circle : IEquatable<Circle>
	{
		/// <summary>
		///     The center position of the circle.
		/// </summary>
		public Point Center;

		/// <summary>
		///     The radius of the circle.
		/// </summary>
		public double Radius;

		/// <summary>
		///     The circumference of the circle.
		/// </summary>
		public double Circumference
		{
			get { return 2*Math.PI*Radius; }
		}

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Circle other)
		{
			return Center.Equals(other.Center) && Radius.Equals(other.Radius);
		}

		/// <summary>
		///     Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Circle && Equals((Circle) obj);
		}

		/// <summary>
		///     Returns the hash code for this instance.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (Center.GetHashCode()*397) ^ Radius.GetHashCode();
			}
		}

		/// <summary>
		///     Tests the two given circles for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Circle left, Circle right)
		{
			return left.Equals(right);
		}

		/// <summary>
		///     Tests the two given circles for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Circle left, Circle right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		///     Calculates a point on this circle with the given angle in radians, clock-wise,
		///     with 0 being (-1, 0).
		/// </summary>
		/// <param name="angle"></param>
		/// <returns></returns>
		[Pure]
		public Point GetPoint(double angle)
		{
			return new Point(-Math.Sin(angle)*Radius,
			                 Math.Cos(angle)*Radius) + (Vector) Center;
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("Center: {0}, Radius: {1}", Center, Radius);
		}
	}
}