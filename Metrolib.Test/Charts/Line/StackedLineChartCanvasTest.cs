using Metrolib.Controls.Charts.Line.Canvas;
using Metrolib.Controls.Charts.Line.Canvas.Stacked;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Line
{
	[TestFixture]
	public sealed class StackedLineChartCanvasTest
		: AbstractLineChartCanvasTest
	{
		protected override AbstractLineChartCanvas Create()
		{
			return new StackedLineChartCanvas();
		}
	}
}