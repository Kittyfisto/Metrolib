using System.Windows;

namespace Metrolib.Controls.Charts.Line.Canvas.Line
{
	/// <summary>
	///     Is responsible for actually drawing points as a line.
	/// </summary>
	public sealed class LineChartCanvas
		: AbstractLineChartCanvas
	{
		static LineChartCanvas()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (LineChartCanvas),
			                                         new FrameworkPropertyMetadata(typeof (LineChartCanvas)));
		}

		/// <summary>
		///     Creates a canvas responsible for drawing the given series only.
		/// </summary>
		/// <param name="series"></param>
		/// <returns></returns>
		protected override AbstractLineSeriesCanvas CreateCanvas(ILineSeries series)
		{
			return new LineSeriesCanvas(series);
		}
	}
}