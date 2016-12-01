using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Metrolib.Controls.Charts.Line.Canvas.Line
{
	/// <summary>
	///     Responsible for drawing an individual <see cref="ILineSeries" /> as a single line and/or area.
	/// </summary>
	public sealed class LineSeriesCanvas
		: AbstractLineSeriesCanvas
	{
		private readonly StreamGeometry _area;
		private readonly StreamGeometry _outline;

		/// <summary>
		///     Initializes this canvas.
		/// </summary>
		/// <param name="lineSeries"></param>
		public LineSeriesCanvas(ILineSeries lineSeries)
			: base(lineSeries)
		{
			_area = new StreamGeometry();
			_outline = new StreamGeometry();
		}

		/// <summary>
		///     Is called to actually draw the series.
		/// </summary>
		/// <param name="drawingContext"></param>
		public override void OnRender(DrawingContext drawingContext)
		{
			if (Series != null)
			{
				List<Point> viewPoints = ProjectToView(Values, Series.Count);

				if (Series.Fill != null)
				{
					CreateArea(viewPoints, Height);
					drawingContext.DrawGeometry(Series.Fill, null, _area);
				}

				if (Series.Outline != null)
				{
					CreateOutline(viewPoints);
					drawingContext.DrawGeometry(null, Series.Outline, _outline);
				}

				if (Series.PointRadius > 0 && (Series.PointFill != null || Series.PointOutline != null))
				{
					DrawPoints(drawingContext, viewPoints);
				}
			}
		}

		private void DrawPoints(DrawingContext drawingContext, IEnumerable<Point> viewPoints)
		{
			foreach (Point point in viewPoints)
			{
				drawingContext.DrawEllipse(Series.PointFill,
				                           Series.PointOutline,
				                           point,
				                           Series.PointRadius,
				                           Series.PointRadius);
			}
		}

		private void CreateOutline(List<Point> viewPoints)
		{
			using (StreamGeometryContext ctx = _outline.Open())
			{
				List<Point>.Enumerator it = viewPoints.GetEnumerator();
				if (it.MoveNext())
				{
					ctx.BeginFigure(it.Current,
					                false, // is NOT filled
					                false); // is NOT closed

					while (it.MoveNext())
					{
						ctx.LineTo(
							it.Current,
							true, // is stroked (line visible)
							false); // is not smoothly joined w/other segments
					}
				}
			}
		}

		private void CreateArea(List<Point> viewPoints, double height)
		{
			using (StreamGeometryContext ctx = _area.Open())
			{
				List<Point>.Enumerator it = viewPoints.GetEnumerator();
				if (it.MoveNext())
				{
					ctx.BeginFigure(new Point(it.Current.X, height),
					                true, // IS filled
					                true); // IS closed

					ctx.LineTo(it.Current,
					           false, // is NOT stroked
					           false); // is not smoothly joined w/other segments

					var point = new Point();
					while (it.MoveNext())
					{
						point = it.Current;
						ctx.LineTo(
							point,
							false, // is NOT stroked
							false); // is not smoothly joined w/other segments
					}

					if (point != new Point())
					{
						ctx.LineTo(new Point(point.X, height),
						           false, // is NOT stroked
						           false); // is not smoothly joined w/other segments
					}
				}
			}
		}
	}
}