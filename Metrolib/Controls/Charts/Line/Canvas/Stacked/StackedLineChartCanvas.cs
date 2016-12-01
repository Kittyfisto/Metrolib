using System;
using System.Collections.Generic;
using System.Linq;

namespace Metrolib.Controls.Charts.Line.Canvas.Stacked
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class StackedLineChartCanvas
		: AbstractLineChartCanvas
	{
		/// <summary>
		///     Is called to prepare the canvas for rendering.
		/// </summary>
		public override void Update()
		{
			StackedLineSeriesCanvas previous = null;
			foreach (StackedLineSeriesCanvas canvas in SeriesCanvasses)
			{
				canvas.Previous = previous;
				canvas.StackWithPrevious();
				previous = canvas;
			}

			base.Update();
		}

		/// <summary>
		///     Is called to determine the range of all series.
		/// </summary>
		protected override void CalculateCombinedRanges()
		{
			if (Series != null)
			{
				IEnumerator<StackedLineSeriesCanvas> it = SeriesCanvasses.Cast<StackedLineSeriesCanvas>().GetEnumerator();
				if (it.MoveNext())
				{
					Range xRange = it.Current.Series.XRange;
					Range yRange = it.Current.StackedYRange;

					while (it.MoveNext())
					{
						xRange.Minimum = Math.Min(xRange.Minimum, it.Current.Series.XRange.Minimum);
						xRange.Maximum = Math.Max(xRange.Maximum, it.Current.Series.XRange.Maximum);

						yRange.Minimum = Math.Min(yRange.Minimum, it.Current.StackedYRange.Minimum);
						yRange.Maximum = Math.Max(yRange.Maximum, it.Current.StackedYRange.Maximum);
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

		protected override AbstractLineSeriesCanvas CreateCanvas(ILineSeries series)
		{
			return new StackedLineSeriesCanvas(series);
		}
	}
}
