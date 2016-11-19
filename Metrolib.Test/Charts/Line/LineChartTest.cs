using System;
using System.Windows;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	public sealed class LineChartTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var chart = new LineChart();
			chart.Series.Should().BeNull();
			chart.XRange.Should().Be(new Range());
			chart.YRange.Should().Be(new Range());
		}

		[Test]
		[STAThread]
		public void TestSeries1()
		{
			var chart = new LineChart();
			new Action(() => chart.Series = null).ShouldNotThrow();
			chart.Series.Should().BeNull();
			chart.XRange.Should().Be(new Range());
			chart.YRange.Should().Be(new Range());
		}

		[Test]
		[STAThread]
		public void TestSeries2()
		{
			var chart = new LineChart();
			new Action(() => chart.Series = new LineSeries[0]).ShouldNotThrow();
			chart.Series.Should().BeEmpty();
			chart.XRange.Should().Be(new Range());
			chart.YRange.Should().Be(new Range());
		}

		[Test]
		[STAThread]
		public void TestSeries3()
		{
			var chart = new LineChart();
			var series = new[] {new LineSeries()};
			new Action(() => chart.Series = series).ShouldNotThrow();
			chart.Series.Should().Equal(series);
			chart.XRange.Should().Be(new Range());
			chart.YRange.Should().Be(new Range());
		}

		[Test]
		[STAThread]
		public void TestSeries4()
		{
			var chart = new LineChart();
			var series = new[] {new LineSeries {Values = new[] {new Point(42, 9001)}}};
			new Action(() => chart.Series = series).ShouldNotThrow();
			chart.Series.Should().Equal(series);
			chart.XRange.Should().Be(new Range(42));
			chart.YRange.Should().Be(new Range(9001));
		}
	}
}