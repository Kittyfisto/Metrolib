// ReSharper disable CheckNamespace

using System;
using System.Diagnostics.Contracts;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public struct Range
		: IEquatable<Range>
	{
		public double Maximum;
		public double Minimum;

		public Range(double minMax)
		{
			Minimum = minMax;
			Maximum = minMax;
		}

		public Range(double minValue, double maxValue)
		{
			Minimum = minValue;
			Maximum = maxValue;
		}

		public bool Equals(Range other)
		{
			return Minimum.Equals(other.Minimum) && Maximum.Equals(other.Maximum);
		}

		public override string ToString()
		{
			return string.Format("[{0}, {1}]", Minimum, Maximum);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Range && Equals((Range) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Minimum.GetHashCode()*397) ^ Maximum.GetHashCode();
			}
		}

		public static bool operator ==(Range left, Range right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Range left, Range right)
		{
			return !left.Equals(right);
		}

		[Pure]
		public double GetRelative(double value)
		{
			return (value - Minimum)/(Maximum - Minimum);
		}
	}
}