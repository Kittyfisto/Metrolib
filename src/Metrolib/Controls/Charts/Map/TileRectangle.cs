using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using GeoVis;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	/// </summary>
	public struct TileRectangle
		: IEnumerable<Tile>, IEquatable<TileRectangle>
	{
		/// <summary>
		/// </summary>
		public int X => _x;

		/// <summary>
		/// </summary>
		public int Y => _y;

		/// <summary>
		/// </summary>
		public int Z => _z;

		/// <summary>
		/// </summary>
		public int Width => _width;

		/// <summary>
		/// </summary>
		public int Height => _height;

		private readonly int _x;
		private readonly int _y;
		private readonly int _z;
		private readonly int _width;
		private readonly int _height;

		/// <summary>
		/// </summary>
		/// <param name="x">The smallest x coordinate of the rectangle</param>
		/// <param name="y">The smallest y coordinate of the rectangle</param>
		/// <param name="z">The depth of the rectangle</param>
		/// <param name="width">The number of tiles on the x-axis of this rectangle</param>
		/// <param name="height">The number of tiles on the y-axis of this rectangle</param>
		public TileRectangle(int x, int y, int z, int width, int height)
		{
			_x = x;
			_y = y;
			_z = z;
			_width = width;
			_height = height;
		}

		/// <summary>
		///     Tests if the given tile is part of this rectangle.
		/// </summary>
		/// <remarks>
		///     A tile must be on the same z-plane in orer to be considered inside a rectangle.
		/// </remarks>
		/// <param name="tile"></param>
		/// <returns></returns>
		[Pure]
		public bool Contains(Tile tile)
		{
			return tile.X >= _x && tile.X <= _x + _width &&
			       tile.Y >= _y && tile.Y <= _y + _height &&
			       tile.Z == _z;
		}

		/// <summary>
		/// </summary>
		public int Count => _width * _height;

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public static readonly TileRectangle Empty = new TileRectangle();

		#region Overrides of ValueType

		/// <inheritdoc />
		public override string ToString()
		{
			return string.Format("X: {0}, Y: {1}, Width: {2}, Height: {3}, Z: {4}",
			                     _x, _y,
			                     _width, _height,
			                     _z);
		}

		#endregion

		#region Equality members

		/// <inheritdoc />
		public bool Equals(TileRectangle other)
		{
			return _x == other._x && _y == other._y && _z == other._z && _width == other._width && _height == other._height;
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(objA: null, objB: obj)) return false;
			return obj is TileRectangle rectangle && Equals(rectangle);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _x;
				hashCode = (hashCode * 397) ^ _y;
				hashCode = (hashCode * 397) ^ _z;
				hashCode = (hashCode * 397) ^ _width;
				hashCode = (hashCode * 397) ^ _height;
				return hashCode;
			}
		}

		/// <summary>
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(TileRectangle left, TileRectangle right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(TileRectangle left, TileRectangle right)
		{
			return !left.Equals(right);
		}

		#endregion

		#region Implementation of IEnumerable

		/// <inheritdoc />
		public IEnumerator<Tile> GetEnumerator()
		{
			for (var y = _y; y < _y + _height; ++y)
			for (var x = _x; x < _x + _width; ++x)
			{
				var tile = new Tile(x, y, _z);
				yield return tile;
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		/// <summary>
		/// </summary>
		/// <param name="rectangle"></param>
		/// <param name="zoomLevel"></param>
		/// <returns></returns>
		public static TileRectangle CreateFrom(MercatorRectangle rectangle, int zoomLevel)
		{
			var zoomFactor = Math.Pow(x: 2, y: zoomLevel);
			var tileEdgeLength = MercatorRectangle.Earth.Width / zoomFactor;

			// Given zoom level = 1
			// (X: 0, Y: 0) is top left
			// (X: 1, Y: 0) is top right
			// (X: 1, Y: 1) is bottom right
			// => We can interpolate

			var xDifference = rectangle.Min.X - MercatorRectangle.Earth.Min.X;
			var yDifference = rectangle.Min.Y - MercatorRectangle.Earth.Min.Y;
			var x = (int) (xDifference / tileEdgeLength);
			var y = (int) (yDifference / tileEdgeLength);
			var width = (int) Math.Ceiling(rectangle.Width / tileEdgeLength);
			var height = (int) Math.Ceiling(rectangle.Height / tileEdgeLength);
			return new TileRectangle(x, y, zoomLevel, width, height);
		}
	}
}