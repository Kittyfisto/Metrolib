using System.Windows;
using FluentAssertions;
using Metrolib.Geometry;
using NUnit.Framework;

namespace Metrolib.Test.Geometry
{
	[TestFixture]
	public sealed class TriangleTest
	{
		[Test]
		public void TestContains1()
		{
			var triangle = new Triangle
				{
					P0 = new Point(0, 0),
					P1 = new Point(5, 0),
					P2 = new Point(0, 5)
				};
			triangle.Contains(triangle.P0).Should().BeTrue("because a triangle should contain its corners");
			triangle.Contains(triangle.P1).Should().BeTrue("because a triangle should contain its corners");
			triangle.Contains(triangle.P2).Should().BeTrue("because a triangle should contain its corners");

			triangle.Contains(new Point(1, 1)).Should().BeTrue();

			triangle.Contains(new Point(-0.1, 0)).Should().BeFalse();
			triangle.Contains(new Point(5.1, 0)).Should().BeFalse();

			triangle.Contains(new Point(0, -0.1)).Should().BeFalse();
			triangle.Contains(new Point(0, 5.1)).Should().BeFalse();
		}
	}
}