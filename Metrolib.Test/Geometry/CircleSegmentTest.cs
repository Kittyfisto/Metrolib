using System;
using System.Windows;
using FluentAssertions;
using Metrolib.Geometry;
using NUnit.Framework;

namespace Metrolib.Test.Geometry
{
	[TestFixture]
	public sealed class CircleSegmentTest
	{
		[Test]
		public void Ctor()
		{
			var segment = new CircleSegment();
			segment.Circle.Center.Should().Be(new Point(0, 0));
			segment.Circle.Radius.Should().Be(0);
			segment.StartAngle.Should().Be(0);
			segment.EndAngle.Should().Be(0);
			segment.StartPoint.Should().Be(new Point(0, 0));
			segment.EndPoint.Should().Be(new Point(0, 0));
		}

		[Test]
		public void TestContains1()
		{
			var segment = new CircleSegment
				{
					Circle =
						{
							Center = new Point(0, 0),
							Radius = 5
						},
					StartAngle = Math.PI/2,
					EndAngle = Math.PI
				};

			segment.Contains(segment.Circle.Center).Should().BeTrue("because a circle segment should contain its center");
			segment.Contains(segment.StartPoint).Should().BeTrue("because a circle segment should contain its corners");
			segment.Contains(segment.EndPoint).Should().BeTrue("because a circle segment should contain its corners");

			segment.Contains(new Point(-2, -2)).Should().BeTrue("because the point lies in the center of the circle segment");
			segment.Contains(new Point(-3, -3)).Should().BeTrue("because the point lies in the center of the circle segment");

			segment.Contains(new Point(6, 0)).Should().BeFalse();
			segment.Contains(new Point(-6, 0)).Should().BeFalse();
			segment.Contains(new Point(0, 6)).Should().BeFalse();
			segment.Contains(new Point(0, -6)).Should().BeFalse();

			segment.Contains(new Point(2, 2)).Should().BeFalse("because the point lies in the wrong slice of the circle");
			segment.Contains(new Point(-2, 2)).Should().BeFalse("because the point lies in the wrong slice of the circle");
			segment.Contains(new Point(2, -2)).Should().BeFalse("because the point lies in the wrong slice of the circle");
		}

		[Test]
		public void TestContains2()
		{
			var segment = new CircleSegment
				{
					Circle =
						{
							Center = new Point(0, 0),
							Radius = 5
						},
					StartAngle = Math.PI,
					EndAngle = Math.PI*1.5
				};

			segment.Contains(new Rect(0, 0, 0, 0)).Should().BeTrue();
			segment.Contains(new Rect(0, -1.1, 1, 1)).Should().BeTrue();
		}

		[Test]
		public void TestContains3()
		{
			var segment = new CircleSegment
			{
				Circle =
				{
					Center = new Point(300, 300),
					Radius = 200
				},
				StartAngle = 0,
				EndAngle = Math.PI/2
			};
			segment.Contains(new Point(250, 350)).Should().BeTrue();
		}
	}
}