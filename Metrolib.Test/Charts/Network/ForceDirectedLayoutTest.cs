using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network
{
	[TestFixture]
	public sealed class ForceDirectedLayoutTest
	{
		[Test]
		public void TestCtor()
		{
			var layout = new ForceDirectedLayout();
			layout.Repulsiveness.Should().Be(1000);
			layout.SpringStiffness.Should().Be(5);
			layout.SpringDampening.Should().Be(2);
			layout.Distance.Should().Be(100);
		}

		[Test]
		public void TestRepulsiveness()
		{
			var layout = new ForceDirectedLayout();
			var changes = new List<string>();
			layout.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			layout.Repulsiveness = 1001;
			changes.Should().Equal(new object[] {"Repulsiveness"});

			layout.Repulsiveness = 1001;
			changes.Should().Equal(new object[] { "Repulsiveness" });

			layout.Repulsiveness = 999;
			changes.Should().Equal(new object[] { "Repulsiveness", "Repulsiveness" });
		}

		[Test]
		public void TestSpringStiffness()
		{
			var layout = new ForceDirectedLayout();
			var changes = new List<string>();
			layout.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			layout.SpringStiffness = 1001;
			changes.Should().Equal(new object[] { "SpringStiffness" });

			layout.SpringStiffness = 1001;
			changes.Should().Equal(new object[] { "SpringStiffness" });

			layout.SpringStiffness = 999;
			changes.Should().Equal(new object[] { "SpringStiffness", "SpringStiffness" });
		}

		[Test]
		public void TestSpringDampening()
		{
			var layout = new ForceDirectedLayout();
			var changes = new List<string>();
			layout.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			layout.SpringDampening = 1001;
			changes.Should().Equal(new object[] { "SpringDampening" });

			layout.SpringDampening = 1001;
			changes.Should().Equal(new object[] { "SpringDampening" });

			layout.SpringDampening = 999;
			changes.Should().Equal(new object[] { "SpringDampening", "SpringDampening" });
		}

		[Test]
		public void TestDistance()
		{
			var layout = new ForceDirectedLayout();
			var changes = new List<string>();
			layout.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			layout.Distance = 1001;
			changes.Should().Equal(new object[] { "Distance" });

			layout.Distance = 1001;
			changes.Should().Equal(new object[] { "Distance" });

			layout.Distance = 999;
			changes.Should().Equal(new object[] { "Distance", "Distance" });
		}
	}
}