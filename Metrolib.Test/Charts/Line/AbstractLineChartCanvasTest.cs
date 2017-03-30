using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls.Charts.Line.Canvas;
using Moq;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public abstract class AbstractLineChartCanvasTest
	{
		protected abstract AbstractLineChartCanvas Create();

		[Test]
		public void TestCtor()
		{
			var canvas = Create();
			canvas.Series.Should().BeNull();
			canvas.SeriesCanvasses.Should().BeEmpty();
		}

		[Test]
		public void TestSeries1()
		{
			var canvas = Create();
			new Action(() => canvas.Series = null).ShouldNotThrow();
			canvas.Series.Should().BeNull();
			canvas.SeriesCanvasses.Should().BeEmpty();
		}

		[Test]
		public void TestSeries2()
		{
			var canvas = Create();
			new Action(() => canvas.Series = new ILineSeries[0]).ShouldNotThrow();
			canvas.Series.Should().BeEmpty();
			canvas.SeriesCanvasses.Should().BeEmpty();
		}

		[Test]
		public void TestSeries3()
		{
			var canvas = Create();
			new Action(() => canvas.Series = new List<ILineSeries>()).ShouldNotThrow();
			canvas.Series.Should().BeEmpty();
			canvas.SeriesCanvasses.Should().BeEmpty();
		}

		[Test]
		public void TestSeries4()
		{
			var canvas = Create();
			var series = new Mock<ILineSeries>();
			new Action(() => canvas.Series = new[] {series.Object}).ShouldNotThrow();
			canvas.Series.Should().Equal(new object[] {series.Object});
			canvas.SeriesCanvasses.Count().Should().Be(1);
		}

		[Test]
		public void TestSeries5()
		{
			var canvas = Create();
			var series = new Mock<ILineSeries>();
			var serieses = new ObservableCollection<ILineSeries>();
			new Action(() => canvas.Series = serieses).ShouldNotThrow();
			canvas.Series.Should().BeEmpty();
			canvas.SeriesCanvasses.Should().BeEmpty();

			serieses.Add(series.Object);
			canvas.Series.Should().Equal(new object[] {series.Object});
			canvas.SeriesCanvasses.Count().Should().Be(1);
		}

		[Test]
		public void TestXAxisTicks()
		{
			var canvas = Create();
			canvas.Series = new[]
				{
					new LineSeries
						{
							Values = new[]
								{
									new Point(1, -2),
									new Point(2, 4)
								}
						}
				};

			canvas.Arrange(new Rect(0, 0, 1024, 768));

			canvas.Update();
			canvas.XRange.Should().Be(new Range(1, 2));
		}
	}
}