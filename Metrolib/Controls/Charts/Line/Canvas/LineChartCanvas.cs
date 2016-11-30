using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
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

		sealed class LineSeriesCanvas
			: AbstractLineSeriesCanvas
		{
			private readonly StreamGeometry _area;
			private readonly StreamGeometry _outline;

			public LineSeriesCanvas(ILineSeries lineSeries)
				: base(lineSeries)
			{
				_area = new StreamGeometry();
				_outline = new StreamGeometry();
			}

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
				foreach (var point in viewPoints)
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