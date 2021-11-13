using FluentAssertions;
using GeoVis;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Map
{
	[TestFixture]
	public sealed class TileRectangleTest
	{
		[Test]
		public void TestCreateFrom1()
		{
			var rectangle = MercatorRectangle.Earth;
			var tiles = TileRectangle.CreateFrom(rectangle, 0);
			tiles.X.Should().Be(0);
			tiles.Y.Should().Be(0);
			tiles.Z.Should().Be(0);
			tiles.Width.Should().Be(1);
			tiles.Height.Should().Be(1);
			tiles.Count.Should().Be(1);
		}

		[Test]
		public void TestCreateFrom2()
		{
			var rectangle = MercatorRectangle.Earth;
			var tiles = TileRectangle.CreateFrom(rectangle, 1);
			tiles.X.Should().Be(0);
			tiles.Y.Should().Be(0);
			tiles.Z.Should().Be(1);
			tiles.Width.Should().Be(2);
			tiles.Height.Should().Be(2);
			tiles.Count.Should().Be(4);
		}

		[Test]
		public void TestCreateFrom3()
		{
			var rectangle = MercatorRectangle.Earth;
			var tiles = TileRectangle.CreateFrom(rectangle, 2);
			tiles.X.Should().Be(0);
			tiles.Y.Should().Be(0);
			tiles.Z.Should().Be(2);
			tiles.Width.Should().Be(4);
			tiles.Height.Should().Be(4);
			tiles.Count.Should().Be(16);
		}
	}
}