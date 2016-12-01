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
		///     Creates a canvas responsible for drawing the given series only.
		/// </summary>
		/// <param name="series"></param>
		/// <returns></returns>
		protected override AbstractLineSeriesCanvas CreateCanvas(ILineSeries series)
		{
			return new StackedLineSeriesCanvas(series);
		}
	}
}
