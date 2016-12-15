using System;
using System.Windows;
using FluentAssertions;
using Metrolib.Geometry;
using NUnit.Framework;

namespace Metrolib.Test.Geometry
{
	[TestFixture]
	public sealed class CircleTest
	{
		[Test]
		public void TestGetPoint1()
		{
			var circle = new Circle
				{
					Center = new Point(1, 2),
					Radius = 0
				};
			circle.GetPoint(0).Should().Be(circle.Center);
		}

		[Test]
		public void TestGetCircumference()
		{
			new Circle {Radius = 0}.Circumference.Should().Be(0);
			new Circle{Radius = 1}.Circumference.Should().Be(2*Math.PI);
			new Circle {Radius = 0.5}.Circumference.Should().Be(Math.PI);
		}
	}
}