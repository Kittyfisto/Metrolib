using System.Collections.Generic;
using System.Windows.Media;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	public sealed class AxisTest
	{
		[Test]
		public void TestAxis()
		{
			var axis = new AxisDefinition();
			axis.ShowLines.Should().BeTrue();
			axis.ShowTicks.Should().BeTrue();
			axis.Spacing.Should().Be(100);
			axis.Caption.Should().BeNull();
			axis.LinePen.Should().NotBeNull();
			axis.LinePen.Thickness.Should().Be(1);
			axis.LinePen.Brush.Should().NotBeNull();
		}

		[Test]
		public void TestSpacing()
		{
			var axis = new AxisDefinition();
			var changes = new List<string>();
			axis.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			axis.Spacing = 50;
			axis.Spacing.Should().Be(50);
			changes.Should().Equal(new string[] {"Spacing"});

			axis.Spacing = 50;
			changes.Should().Equal(new string[] { "Spacing" });

			axis.Spacing = 50.00001;
			axis.Spacing.Should().Be(50.00001);
			changes.Should().Equal(new string[] { "Spacing", "Spacing" });
		}

		[Test]
		public void TestCaption()
		{
			var axis = new AxisDefinition();
			var changes = new List<string>();
			axis.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			axis.Caption = "foo";
			axis.Caption.Should().Be("foo");
			changes.Should().Equal(new string[] { "Caption" });

			axis.Caption = "foo";
			changes.Should().Equal(new string[] { "Caption" });

			axis.Caption = "bar";
			axis.Caption.Should().Be("bar");
			changes.Should().Equal(new string[] { "Caption", "Caption" });
		}

		[Test]
		public void TestShowTicks()
		{
			var axis = new AxisDefinition();
			var changes = new List<string>();
			axis.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			axis.ShowTicks = false;
			axis.ShowTicks.Should().BeFalse();
			changes.Should().Equal(new string[] { "ShowTicks" });

			axis.ShowTicks = false;
			changes.Should().Equal(new string[] { "ShowTicks" });

			axis.ShowTicks = true;
			axis.ShowTicks.Should().BeTrue();
			changes.Should().Equal(new string[] { "ShowTicks", "ShowTicks" });
		}

		[Test]
		public void TestShowLines()
		{
			var axis = new AxisDefinition();
			var changes = new List<string>();
			axis.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			axis.ShowLines = false;
			axis.ShowLines.Should().BeFalse();
			changes.Should().Equal(new string[] { "ShowLines" });

			axis.ShowLines = false;
			changes.Should().Equal(new string[] { "ShowLines" });

			axis.ShowLines = true;
			axis.ShowLines.Should().BeTrue();
			changes.Should().Equal(new string[] { "ShowLines", "ShowLines" });
		}

		[Test]
		public void TestLinePen()
		{
			var axis = new AxisDefinition();
			var changes = new List<string>();
			axis.PropertyChanged += (sender, args) => changes.Add(args.PropertyName);

			var pen = new Pen(Brushes.Blue, 2);

			axis.LinePen = pen;
			axis.LinePen.Should().Be(pen);
			changes.Should().Equal(new string[] { "LinePen" });

			axis.LinePen = pen;
			changes.Should().Equal(new string[] { "LinePen" });

			var pen2 = new Pen(Brushes.Black, 2);

			axis.LinePen = pen2;
			axis.LinePen.Should().Be(pen2);
			changes.Should().Equal(new string[] { "LinePen", "LinePen" });
		}
	}
}