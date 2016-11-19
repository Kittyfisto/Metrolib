using System;
using System.Windows;
using System.Windows.Media;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	public sealed class LineSeriesTest
	{
		[Test]
		public void TestCtor()
		{
			var series = new LineSeries();
			series.Fill.Should().BeNull();
			series.Outline.Should().NotBeNull();
			series.Outline.Brush.Should().Be(Brushes.DodgerBlue);
			series.Outline.Thickness.Should().Be(2);
		}

		[Test]
		public void TestValues1()
		{
			var series = new LineSeries();
			new Action(() => series.Values = null).ShouldNotThrow();
			series.Values.Should().BeNull();
			series.XRange.Should().Be(new Range());
			series.YRange.Should().Be(new Range());
		}

		[Test]
		public void TestValues2()
		{
			var series = new LineSeries();
			new Action(() => series.Values = new Point[0]).ShouldNotThrow();
			series.Values.Should().BeEmpty();
			series.XRange.Should().Be(new Range());
			series.YRange.Should().Be(new Range());
		}

		[Test]
		public void TestValues3()
		{
			var series = new LineSeries();
			new Action(() => series.Values = new[] {new Point(1, 2)}).ShouldNotThrow();
			series.Values.Should().Equal(new object[] {new Point(1, 2)});
			series.XRange.Should().Be(new Range(1, 1));
			series.YRange.Should().Be(new Range(2, 2));
		}

		[Test]
		public void TestValues4()
		{
			var series = new LineSeries();
			new Action(() => series.Values = new[] {new Point(-1, 2), new Point(2, 4)}).ShouldNotThrow();
			series.Values.Should().Equal(new object[] { new Point(-1, 2), new Point(2, 4)});
			series.XRange.Should().Be(new Range(-1, 2));
			series.YRange.Should().Be(new Range(2, 4));
		}
	}
}