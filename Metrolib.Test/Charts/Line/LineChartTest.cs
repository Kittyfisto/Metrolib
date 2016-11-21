using System;
using System.Collections.ObjectModel;
using System.Windows;
using FluentAssertions;
using Moq;
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

		[Test]
		[STAThread]
		[Description("Verifies that any ILineSeries implementation can be attached to a line chart")]
		public void TestSeries5()
		{
			var chart = new LineChart();

			var series1 = new Mock<ILineSeries>();
			new Action(() => chart.Series = new[] { series1.Object }).ShouldNotThrow();

			// In order to obtain full coverage, we must attach a 2nd list to test
			// that we correctly use OldValue when detaching from events of the old list
			var series2 = new Mock<ILineSeries>();
			new Action(() => chart.Series = new[] { series2.Object }).ShouldNotThrow(
				"because we should be able to exchange on ILineSeries implementation for another");
		}

		[Test]
		[STAThread]
		[Description("Verifies that any ILineSeries implementation can be attached to a line chart")]
		public void TestSeries6()
		{
			var chart = new LineChart();

			var series1 = new Mock<ILineSeries>();
			var series = new ObservableCollection<ILineSeries>();
			chart.Series = series;

			new Action(() => series.Add(series1.Object))
				.ShouldNotThrow("because we should be able to add any ILineSeries implementation");
			new Action(() => series.Remove(series1.Object))
				.ShouldNotThrow("because we should be able to add any ILineSeries implementation");
		}
	}
}