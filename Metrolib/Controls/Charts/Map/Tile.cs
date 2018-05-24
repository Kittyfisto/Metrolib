using System;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public struct Tile : IEquatable<Tile>
	{
		#region Overrides of ValueType

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Format("X: {0}, Y: {1}, Z: {2}", X, Y, Z);
		}

		#endregion

		#region Equality members

		/// <inheritdoc />
		public bool Equals(Tile other)
		{
			return X == other.X && Y == other.Y && Z == other.Z;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			return obj is Tile tile && Equals(tile);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = X;
				hashCode = (hashCode * 397) ^ Y;
				hashCode = (hashCode * 397) ^ Z;
				return hashCode;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Tile left, Tile right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Tile left, Tile right)
		{
			return !left.Equals(right);
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public readonly int X;

		/// <summary>
		/// 
		/// </summary>
		public readonly int Y;

		/// <summary>
		/// 
		/// </summary>
		public readonly int Z;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
		public Tile(int x, int y, int z)
		{
			X = x;
			Y = y;
			Z = z;
		}
	}
}