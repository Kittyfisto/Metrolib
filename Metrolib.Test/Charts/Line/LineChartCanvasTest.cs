using System;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls.Charts.Line.Canvas;
using Metrolib.Controls.Charts.Line.Canvas.Line;
using Moq;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	public sealed class LineChartCanvasTest
		: AbstractLineChartCanvasTest
	{
		protected override AbstractLineChartCanvas Create()
		{
			return new LineChartCanvas();
		}

		[Test]
		[STAThread]
		public void TestLineSeries1()
		{
			var canvas = Create();

			canvas.Series = new[] {new LineSeries {Values = new[] {new Point(1, 3), new Point(2, 4)}}};

			canvas.Update();
			canvas.XRange.Should().Be(new Range(1, 2));
			canvas.YRange.Should().Be(new Range(3, 4));
		}
	}
}