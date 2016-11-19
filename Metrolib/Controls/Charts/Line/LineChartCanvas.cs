using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Metrolib.Controls.Charts.Line
{
	/// <summary>
	///     Is responsible for actually drawing points as a line.
	/// </summary>
	internal sealed class LineChartCanvas
		: Control
	{
		public static readonly DependencyProperty SeriesProperty =
			DependencyProperty.Register("Series", typeof (LineSeries), typeof (LineChartCanvas), new PropertyMetadata(null, OnSeriesChanged));

		private static void OnSeriesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((LineChartCanvas) dependencyObject).OnSeriesChanged();
		}

		private void OnSeriesChanged()
		{
			InvalidateVisual();
		}

		public LineSeries Series
		{
			get { return (LineSeries) GetValue(SeriesProperty); }
			set { SetValue(SeriesProperty, value); }
		}

		public static readonly DependencyProperty FillProperty =
			DependencyProperty.Register("Fill", typeof (Brush), typeof (LineChartCanvas),
			                            new PropertyMetadata(Brushes.LightSkyBlue));

		public static readonly DependencyProperty OutlineProperty =
			DependencyProperty.Register("Outline", typeof (Pen), typeof (LineChartCanvas),
			                            new PropertyMetadata(new Pen(Brushes.DodgerBlue, 2)));

		private readonly StreamGeometry _area;
		private readonly StreamGeometry _outline;

		static LineChartCanvas()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (LineChartCanvas),
			                                         new FrameworkPropertyMetadata(typeof (LineChartCanvas)));
		}

		public LineChartCanvas()
		{
			_outline = new StreamGeometry();
			_area = new StreamGeometry();
		}

		public Pen Outline
		{
			get { return (Pen) GetValue(OutlineProperty); }
			set { SetValue(OutlineProperty, value); }
		}

		public Brush Fill
		{
			get { return (Brush) GetValue(FillProperty); }
			set { SetValue(FillProperty, value); }
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			if (Series != null)
			{
				List<Point> viewPoints = Series.ProjectToView(ActualWidth, ActualHeight);

				if (Fill != null)
				{
					CreateArea(viewPoints);
					drawingContext.DrawGeometry(Fill, null, _area);
				}

				if (Outline != null)
				{
					CreateOutline(viewPoints);
					drawingContext.DrawGeometry(null, Outline, _outline);
				}
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

		private void CreateArea(List<Point> viewPoints)
		{
			using (StreamGeometryContext ctx = _area.Open())
			{
				List<Point>.Enumerator it = viewPoints.GetEnumerator();
				if (it.MoveNext())
				{
					ctx.BeginFigure(new Point(it.Current.X, ActualHeight),
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
						ctx.LineTo(new Point(point.X, ActualHeight),
						           false, // is NOT stroked
						           false); // is not smoothly joined w/other segments
					}
				}
			}
		}
	}
}