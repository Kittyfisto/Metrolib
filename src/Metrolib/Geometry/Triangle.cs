using System;
using System.Diagnostics.Contracts;
using System.Windows;

namespace Metrolib.Geometry
{
	/// <summary>
	///     A 2-d triangle.
	/// </summary>
	public struct Triangle
		: IEquatable<Triangle>
	{
		/// <summary>
		///     First point of the triangle.
		/// </summary>
		public Point P0;

		/// <summary>
		///     Second point of the triangle.
		/// </summary>
		public Point P1;

		/// <summary>
		///     Third point of the triangle.
		/// </summary>
		public Point P2;

		/// <summary>
		///     Initializes this triangle.
		/// </summary>
		/// <param name="p0"></param>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		public Triangle(Point p0, Point p1, Point p2)
		{
			P0 = p0;
			P1 = p1;
			P2 = p2;
		}

		/// <summary>
		///     Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Triangle other)
		{
			return P0.Equals(other.P0) && P1.Equals(other.P1) && P2.Equals(other.P2);
		}

		/// <summary>
		///     Determines the side of a half plane (determined by <paramref name="p1" /> and <paramref name="p2" />)
		///     a given point (<paramref name="p0" />) lies.
		/// </summary>
		/// <param name="p0"></param>
		/// <param name="p1"></param>
		/// <param name="p2"></param>
		/// <returns></returns>
		public static double Sign(Point p0, Point p1, Point p2)
		{
			return (p0.X - p2.X)*(p1.Y - p2.Y) - (p1.X - p2.X)*(p0.Y - p2.Y);
		}

		/// <summary>
		///     Tests if this triangle contains the given point, or not.
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		[Pure]
		public bool Contains(Point that)
		{
			bool b1 = Sign(that, P0, P1) < 0.0;
			bool b2 = Sign(that, P1, P2) < 0.0;
			bool b3 = Sign(that, P2, P0) < 0.0;

			return (b1 == b2) && (b2 == b3);
		}

		/// <summary>
		///     Tests if this triangle fully contains the given rectangle, or not.
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		[Pure]
		public bool Contains(Rect that)
		{
			return Contains(that.TopLeft) &&
			       Contains(that.TopRight) &&
			       Contains(that.BottomRight) &&
			       Contains(that.BottomLeft);
		}

		/// <summary>
		///     Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Triangle && Equals((Triangle) obj);
		}

		/// <summary>
		///     Returns the hash code for this instance.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			unchecked
			{
				int hashCode = P0.GetHashCode();
				hashCode = (hashCode*397) ^ P1.GetHashCode();
				hashCode = (hashCode*397) ^ P2.GetHashCode();
				return hashCode;
			}
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0} {1} {2}", P0, P1, P2);
		}

		/// <summary>
		///     Tests the two given triangles for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Triangle left, Triangle right)
		{
			return left.Equals(right);
		}

		/// <summary>
		///     Tests the two given triangles for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Triangle left, Triangle right)
		{
			return !left.Equals(right);
		}
	}
}