using System;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls.Charts.Network.Algorithms;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network.Algorithms
{
	[TestFixture]
	public sealed class SpringTest
	{
		private Random _rng;

		[SetUp]
		public void Setup()
		{
			_rng = new Random(42);
		}

		[Test]
		public void TestGetForce1()
		{
			var p0s = new[]
				{
					new Point(0, 0),
					new Point(1, 0),
					new Point(0, 1),
				};
			var p1s = new[]
				{
					new Point(1, 0),
					new Point(2, 0),
					new Point(1, 1),
				};

			var spring = new Spring(_rng, 1, 1, 1);
			for (int i = 0; i < p0s.Length; ++i)
			{
				spring.GetForce(p0s[i], p1s[i], new Vector(0, 0)).Should().Be(new Vector(0, 0), "because a spring with its vertices at rest length should not be under any stress");
			}
		}

		[Test]
		public void TestGetForce2()
		{
			var spring = new Spring(_rng, 1, 10, 1);
			var force = spring.GetForce(new Point(0, 0), new Point(5, 0), new Vector(0, 0));
			force.X.Should().Be(-5);
			force.Y.Should().Be(0);
		}
	}
}