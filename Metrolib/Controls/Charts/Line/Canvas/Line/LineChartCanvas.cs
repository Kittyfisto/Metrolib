using System;
using System.Collections.Generic;
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
		///     Is called to determine the range of all series.
		/// </summary>
		protected override void CalculateCombinedRanges()
		{
			if (Series != null)
			{
				IEnumerator<ILineSeries> it = Series.GetEnumerator();
				if (it.MoveNext())
				{
					Range xRange = it.Current.XRange;
					Range yRange = it.Current.YRange;

					while (it.MoveNext())
					{
						xRange.Minimum = Math.Min(xRange.Minimum, it.Current.XRange.Minimum);
						xRange.Maximum = Math.Max(xRange.Maximum, it.Current.XRange.Maximum);

						yRange.Minimum = Math.Min(yRange.Minimum, it.Current.YRange.Minimum);
						yRange.Maximum = Math.Max(yRange.Maximum, it.Current.YRange.Maximum);
					}

					XRange = xRange;
					YRange = yRange;
				}
				else
				{
					XRange = new Range();
					YRange = new Range();
				}
			}
			else
			{
				XRange = new Range();
				YRange = new Range();
			}
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