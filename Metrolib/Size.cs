using System;
using System.Diagnostics.Contracts;

namespace Metrolib
{
	/// <summary>
	///     This structure is meant to hold an amount of information, similar to
	///     <see cref="TimeSpan" /> holding an amount of time.
	/// </summary>
	/// <remarks>
	///     Values can be created from <see cref="FromBytes" />, etc.. and the amount of
	///     bytes, kilobytes, etc.. can be retrieved from <see cref="Bytes" />
	/// </remarks>
	public struct Size : IEquatable<Size>
	{
		/// <summary>
		///     Exactly zero information.
		/// </summary>
		public static readonly Size Zero;

		/// <summary>
		///     One byte.
		/// </summary>
		public static readonly Size OneByte;

		/// <summary>
		///     One kilobyte (1024 bytes).
		/// </summary>
		public static readonly Size OneKilobyte;

		/// <summary>
		///     One megabyte (1024² bytes).
		/// </summary>
		public static readonly Size OneMegabyte;

		/// <summary>
		///     One gigabyte (1024³ bytes).
		/// </summary>
		public static readonly Size OneGigabyte;

		private readonly ulong _numBytes;

		static Size()
		{
			Zero = new Size();
			OneByte = new Size(1);
			OneKilobyte = new Size(1024);
			OneMegabyte = new Size(1024*1024);
			OneGigabyte = new Size(1024*1024*1024);
		}

		private Size(ulong numBytes)
		{
			_numBytes = numBytes;
		}

		/// <summary>
		///     The total amount of bytes in this <see cref="Size" />.
		/// </summary>
		public ulong Bytes
		{
			get { return _numBytes; }
		}

		/// <summary>
		///     The total amount of kilobytes in this <see cref="Size" />.
		/// </summary>
		public double Kilobytes
		{
			get { return this/OneKilobyte; }
		}

		/// <summary>
		///     The total amount of megaybtes in this <see cref="Size" />.
		/// </summary>
		public double Megabytes
		{
			get { return this/OneMegabyte; }
		}

		/// <summary>
		///     The total amount of gigabytes in this <see cref="Size" />.
		/// </summary>
		public double Gigabytes
		{
			get { return this/OneGigabyte; }
		}

		/// <summary>
		///     Compares this value against the given one for equality.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public bool Equals(Size other)
		{
			return _numBytes == other._numBytes;
		}

		/// <summary>
		///     Compares this value against the given one for equality.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Size && Equals((Size) obj);
		}

		/// <summary>
		///     Computes the hashcode of this size.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return _numBytes.GetHashCode();
		}

		/// <summary>
		///     Tests if the left hand size is greater than the right hand one.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator >(Size lhs, Size rhs)
		{
			return lhs._numBytes > rhs._numBytes;
		}

		/// <summary>
		///     Tests if the left hand size is less than the right hand one.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator <(Size lhs, Size rhs)
		{
			return lhs._numBytes < rhs._numBytes;
		}

		/// <summary>
		///     Tests if the left hand size is greater or equal to the right hand one.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator >=(Size lhs, Size rhs)
		{
			return lhs._numBytes >= rhs._numBytes;
		}

		/// <summary>
		///     Tests if the left hand size is less or equal to the right hand one.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator <=(Size lhs, Size rhs)
		{
			return lhs._numBytes <= rhs._numBytes;
		}

		/// <summary>
		///     Compares two sizes for equality.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator ==(Size lhs, Size rhs)
		{
			return lhs._numBytes == rhs._numBytes;
		}

		/// <summary>
		///     Compares two sizes for inequality.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static bool operator !=(Size lhs, Size rhs)
		{
			return lhs._numBytes != rhs._numBytes;
		}

		/// <summary>
		///     Adds two sizes.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static Size operator +(Size lhs, Size rhs)
		{
			return new Size(lhs._numBytes + rhs._numBytes);
		}

		/// <summary>
		///     Divides two sizes.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static double operator /(Size lhs, Size rhs)
		{
			return 1.0*lhs._numBytes/rhs._numBytes;
		}

		/// <summary>
		/// Multiplies a size by an integer.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static Size operator *(Size lhs, long rhs)
		{
			return FromBytes((long)lhs._numBytes*rhs);
		}

		/// <summary>
		/// Multiplies a size by an integer.
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		public static Size operator *(long lhs, Size rhs)
		{
			return FromBytes(lhs*(long) rhs._numBytes);
		}

		/// <summary>
		///     Converts this value to a user readable string.
		///     Attempts to choose the most appropriate unit for the actual value.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (this > OneGigabyte)
			{
				return string.Format("{0:F2} Gb", this/OneGigabyte);
			}
			if (this > OneMegabyte)
			{
				return string.Format("{0:F2} Mb", this/OneMegabyte);
			}
			if (this > OneKilobyte)
			{
				return string.Format("{0:F2} Kb", this/OneKilobyte);
			}

			return string.Format("{0} bytes", _numBytes);
		}

		/// <summary>
		///     Creates a new <see cref="Size" /> that holds the given amount of bytes.
		/// </summary>
		/// <param name="numBytes"></param>
		/// <returns></returns>
		[Pure]
		public static Size FromBytes(long numBytes)
		{
			return new Size((ulong) numBytes);
		}

		/// <summary>
		///     Creates a new <see cref="Size" /> that holds the given amount of kilobytes.
		/// </summary>
		/// <param name="numKilobytes"></param>
		/// <returns></returns>
		[Pure]
		public static Size FromKilobytes(long numKilobytes)
		{
			return OneKilobyte * numKilobytes;
		}

		/// <summary>
		///     Creates a new <see cref="Size" /> that holds the given amount of megabytes.
		/// </summary>
		/// <param name="numMegabytes"></param>
		/// <returns></returns>
		[Pure]
		public static Size FromMegabytes(long numMegabytes)
		{
			return OneMegabyte*numMegabytes;
		}

		/// <summary>
		///     Creates a new <see cref="Size" /> that holds the given amount of gigabytes.
		/// </summary>
		/// <param name="numGigabytes"></param>
		/// <returns></returns>
		[Pure]
		public static Size FromGigabytes(long numGigabytes)
		{
			return OneGigabyte * numGigabytes;
		}
	}
}