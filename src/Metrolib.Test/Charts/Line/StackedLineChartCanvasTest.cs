using System;
using System.Threading;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls.Charts.Line.Canvas;
using Metrolib.Controls.Charts.Line.Canvas.Stacked;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class StackedLineChartCanvasTest
		: AbstractLineChartCanvasTest
	{
		protected override AbstractLineChartCanvas Create()
		{
			return new StackedLineChartCanvas();
		}

		[Test]
		public void TestLineSeries1()
		{
			var canvas = Create();

			canvas.Series = new[] { new LineSeries { Values = new[] { new Point(1, 2) } } };

			canvas.Update();
			canvas.XRange.Should().Be(new Range(1));
			canvas.YRange.Should().Be(new Range(2));
		}

		[Test]
		public void TestLineSeries2()
		{
			var canvas = Create();

			canvas.Series = new[]
				{
					new LineSeries
						{
							Values = new[]
								{
									new Point(1, 2),
									new Point(4, -9)
								}
						}
				};

			canvas.Update();
			canvas.XRange.Should().Be(new Range(1, 4));
			canvas.YRange.Should().Be(new Range(-9, 2));
		}

		[Test]
		public void TestStackedSeries1()
		{
			var canvas = Create();

			canvas.Series = new[]
				{
					new LineSeries {Values = new[] {new Point(1, 2)}},
					new LineSeries {Values = new[] {new Point(2, 3)}}
				};

			canvas.Update();
			canvas.XRange.Should().Be(new Range(1, 2));
			canvas.YRange.Should().Be(new Range(2, 5));
		}
	}
}