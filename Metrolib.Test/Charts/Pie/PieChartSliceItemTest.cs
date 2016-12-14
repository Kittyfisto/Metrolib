using System;
using System.Windows;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Pie
{
	[TestFixture]
	public sealed class PieChartSliceItemTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var item = new PieChartSliceItem();
			item.StartAngle.Should().Be(0);
			item.OpenAngle.Should().Be(0);
			item.Angle.Should().Be(0);
			item.Direction.Should().Be(SliceDirection.BottomLeft);
		}

		[Test]
		[STAThread]
		public void TestOpenAngle1()
		{
			var item = new PieChartSliceItem {OpenAngle = Math.PI/4};
			item.OpenAngle.Should().Be(Math.PI/4);
			item.Direction.Should().Be(SliceDirection.BottomLeft);
			item.Angle.Should().Be(Math.PI/8);
		}

		[Test]
		[STAThread]
		public void TestStartAngle1()
		{
			var item = new PieChartSliceItem { StartAngle = Math.PI / 4 };
			item.StartAngle.Should().Be(Math.PI / 4);
			item.Direction.Should().Be(SliceDirection.BottomLeft);
			item.Angle.Should().Be(Math.PI / 4);
		}

		[Test]
		[STAThread]
		public void TestStartAngle2()
		{
			var item = new PieChartSliceItem
				{
					StartAngle = Math.PI / 1.5
				};
			item.StartAngle.Should().Be(Math.PI / 1.5);
			item.Direction.Should().Be(SliceDirection.TopLeft);
			item.Angle.Should().Be(Math.PI / 1.5);
		}

		[Test]
		[STAThread]
		public void TestStartAngle3()
		{
			var item = new PieChartSliceItem
			{
				StartAngle = Math.PI * 1.2
			};
			item.StartAngle.Should().Be(Math.PI * 1.2);
			item.Direction.Should().Be(SliceDirection.TopRight);
			item.Angle.Should().Be(Math.PI * 1.2);
		}

		[Test]
		[STAThread]
		public void TestStartAngle4()
		{
			var item = new PieChartSliceItem
			{
				StartAngle = Math.PI * 1.8
			};
			item.StartAngle.Should().Be(Math.PI * 1.8);
			item.Direction.Should().Be(SliceDirection.BottomRight);
			item.Angle.Should().Be(Math.PI * 1.8);
		}

		[Test]
		[STAThread]
		public void TestCenter1()
		{
			var item = new PieChartSliceItem
				{
					Center = new Point(1, 2)
				};
			item.Shape.Circle.Center.Should().Be(new Point(1, 2));
		}
	}
}