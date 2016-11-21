using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
			DependencyProperty.Register("Series", typeof (ILineSeries), typeof (LineChartCanvas),
			                            new PropertyMetadata(null, OnSeriesChanged));

		public static readonly DependencyProperty XRangeProperty =
			DependencyProperty.Register("XRange", typeof (Range), typeof (LineChartCanvas),
			                            new PropertyMetadata(default(Range), OnXRangeChanged));

		public static readonly DependencyProperty YRangeProperty =
			DependencyProperty.Register("YRange", typeof (Range), typeof (LineChartCanvas),
			                            new PropertyMetadata(default(Range), OnYRangeChanged));

		private readonly StreamGeometry _area;
		private readonly StreamGeometry _outline;
		private IEnumerable<Point> _values;

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

		public Range YRange
		{
			get { return (Range) GetValue(YRangeProperty); }
			set { SetValue(YRangeProperty, value); }
		}

		public Range XRange
		{
			get { return (Range) GetValue(XRangeProperty); }
			set { SetValue(XRangeProperty, value); }
		}

		public ILineSeries Series
		{
			get { return (ILineSeries) GetValue(SeriesProperty); }
			set { SetValue(SeriesProperty, value); }
		}

		private static void OnXRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((LineChartCanvas) d).OnXRangeChanged();
		}

		private void OnXRangeChanged()
		{
			InvalidateVisual();
		}

		private static void OnYRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((LineChartCanvas) d).OnYRangeChanged();
		}

		private void OnYRangeChanged()
		{
			InvalidateVisual();
		}

		private static void OnSeriesChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((LineChartCanvas) dependencyObject).OnSeriesChanged((ILineSeries) args.OldValue, (ILineSeries) args.NewValue);
		}

		private void OnSeriesChanged(ILineSeries oldValue, ILineSeries newValue)
		{
			if (oldValue != null)
			{
				oldValue.PropertyChanged -= OnLineSeriesPropertyChanged;
			}
			if (newValue != null)
			{
				newValue.PropertyChanged += OnLineSeriesPropertyChanged;
			}

			if (newValue != null)
			{
				_values = newValue.Values;
			}
			else
			{
				_values = null;
			}

			InvalidateVisual();
		}

		private void OnLineSeriesPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			switch (args.PropertyName)
			{
				case "Values":
					OnValuesChanged(_values, Series.Values);
					break;
			}

			InvalidateVisual();
		}

		private void OnValuesChanged(IEnumerable<Point> oldValue, IEnumerable<Point> newValue)
		{
			var notifiable = oldValue as INotifyCollectionChanged;
			if (notifiable != null)
				notifiable.CollectionChanged -= ValuesOnCollectionChanged;
			notifiable = newValue as INotifyCollectionChanged;
			if (notifiable != null)
				notifiable.CollectionChanged += ValuesOnCollectionChanged;
			_values = newValue;
		}

		private void ValuesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			InvalidateVisual();
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			if (Series != null)
			{
				List<Point> viewPoints = ProjectToView(_values, Series.Count, XRange, YRange, ActualWidth, ActualHeight);

				if (Series.Fill != null)
				{
					CreateArea(viewPoints);
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

		public static List<Point> ProjectToView(IEnumerable<Point> values, int count, Range xRange, Range yRange, double width, double height)
		{
			var ret = new List<Point>(count);
			if (values != null)
			{
				foreach (Point point in values)
				{
					double x = xRange.GetRelative(point.X);
					double y = yRange.GetRelative(point.Y);

					var view = new Point(
						x * width,
						height * (1 - y)
						);
					ret.Add(view);
				}
			}
			return ret;
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