using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class LineChartTest
	{
		[Test]
		public void TestCtor()
		{
			var chart = new LineChart();
			chart.Series.Should().BeNull();
			chart.Canvas.XRange.Should().Be(new Range());
			chart.Canvas.YRange.Should().Be(new Range());
		}

		[Test]
		public void TestSeries1()
		{
			var chart = new LineChart();
			new Action(() => chart.Series = null).Should().NotThrow();
			chart.Series.Should().BeNull();
			chart.Canvas.XRange.Should().Be(new Range());
			chart.Canvas.YRange.Should().Be(new Range());
		}

		[Test]
		public void TestSeries2()
		{
			var chart = new LineChart();
			new Action(() => chart.Series = new LineSeries[0]).Should().NotThrow();
			chart.Series.Should().BeEmpty();
			chart.Canvas.XRange.Should().Be(new Range());
			chart.Canvas.YRange.Should().Be(new Range());
		}

		[Test]
		public void TestSeries3()
		{
			var chart = new LineChart();
			var series = new[] {new LineSeries()};
			new Action(() => chart.Series = series).Should().NotThrow();
			chart.Series.Should().Equal(series);
			chart.Canvas.XRange.Should().Be(new Range());
			chart.Canvas.YRange.Should().Be(new Range());
		}

		[Test]
		public void TestSeries4()
		{
			var chart = new LineChart();
			var series = new[] {new LineSeries {Values = new[] {new Point(42, 9001)}}};

			new Action(() => chart.Series = series).Should().NotThrow();
			chart.Series.Should().Equal(series);

			// We do this via timer now and I don't know yet how to trigger it via unit tests (properly).
			chart.Canvas.Update();

			chart.Canvas.XRange.Should().Be(new Range(42));
			chart.Canvas.YRange.Should().Be(new Range(9001));
		}

		[Test]
		[Description("Verifies that any ILineSeries implementation can be attached to a line chart")]
		public void TestSeries5()
		{
			var chart = new LineChart();

			var series1 = new Mock<ILineSeries>();
			new Action(() => chart.Series = new[] { series1.Object }).Should().NotThrow();

			// In order to obtain full coverage, we must attach a 2nd list to test
			// that we correctly use OldValue when detaching from events of the old list
			var series2 = new Mock<ILineSeries>();
			new Action(() => chart.Series = new[] { series2.Object }).Should().NotThrow(
				"because we should be able to exchange on ILineSeries implementation for another");
		}

		[Test]
		[Description("Verifies that any ILineSeries implementation can be attached to a line chart")]
		public void TestSeries6()
		{
			var chart = new LineChart();

			var series1 = new Mock<ILineSeries>();
			var series = new ObservableCollection<ILineSeries>();
			chart.Series = series;

			new Action(() => series.Add(series1.Object))
				.Should().NotThrow("because we should be able to add any ILineSeries implementation");
			new Action(() => series.Remove(series1.Object))
				.Should().NotThrow("because we should be able to add any ILineSeries implementation");
		}
	}
}