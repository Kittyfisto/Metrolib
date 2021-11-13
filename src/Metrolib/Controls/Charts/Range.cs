// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.Contracts;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Defines a range (minimum, maximum) of <see cref="double" /> values.
	/// </summary>
	public struct Range
		: IEquatable<Range>
	{
		/// <summary>
		///     The biggest value.
		/// </summary>
		public double Maximum;

		/// <summary>
		///     The smallest value.
		/// </summary>
		public double Minimum;

		/// <summary>
		///     Creates a range with the same minimum and maximum.
		/// </summary>
		/// <param name="minMax"></param>
		public Range(double minMax)
		{
			Minimum = minMax;
			Maximum = minMax;
		}

		/// <summary>
		///     Creates a range from the given values.
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		public Range(double minValue, double maxValue)
		{
			Minimum = minValue;
			Maximum = maxValue;
		}

		/// <summary>
		///     Compares this range against the other for equality.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Range other)
		{
			return Minimum.Equals(other.Minimum) && Maximum.Equals(other.Maximum);
		}

		/// <summary>
		///     Converts this value to a user readable string.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("[{0}, {1}]", Minimum, Maximum);
		}

		/// <summary>
		///     Compares this range against the other for equality.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Range && Equals((Range) obj);
		}

		/// <summary>
		///     Computes the hashcode of this range.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (Minimum.GetHashCode()*397) ^ Maximum.GetHashCode();
			}
		}

		/// <summary>
		///     Compares two ranges for equality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Range left, Range right)
		{
			return left.Equals(right);
		}

		/// <summary>
		///     Compares two ranges for inequality.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Range left, Range right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		///     Returns zero if the given value is <see cref="Minimum" />, 0.5 if it's (min+max)/2 and 1 if it's
		///     <see cref="Maximum" />.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[Pure]
		public double GetRelative(double value)
		{
			return (value - Minimum)/(Maximum - Minimum);
		}
	}
}