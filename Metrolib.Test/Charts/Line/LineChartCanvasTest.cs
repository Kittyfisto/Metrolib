using System;
using System.Collections.Generic;
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
			var series = new Mock<ILineSeries>();
			series.Setup(x => x.XRange).Returns(new Range(1, 2));
			series.Setup(x => x.YRange).Returns(new Range(3, 4));

			canvas.Series = new[] {series.Object};

			canvas.Update();
			canvas.XRange.Should().Be(new Range(1, 2));
			canvas.YRange.Should().Be(new Range(3, 4));
		}
	}
}