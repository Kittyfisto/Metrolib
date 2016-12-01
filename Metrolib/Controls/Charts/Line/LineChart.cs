using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Metrolib.Controls.Charts.Line.Canvas;
using Metrolib.Controls.Charts.Line.Canvas.Line;
using Metrolib.Controls.Charts.Line.Canvas.Stacked;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public class LineChart
		: Control
	{
		/// <summary>
		///     Definition of the <see cref="ChartType" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ChartTypeProperty =
			DependencyProperty.Register("ChartType", typeof (LineChartType), typeof (LineChart),
			                            new PropertyMetadata(default(LineChartType), OnChartTypeChanged));

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty SeriesProperty =
			DependencyProperty.Register("Series", typeof (IEnumerable<ILineSeries>), typeof (LineChart),
			                            new PropertyMetadata(null, OnSeriesChanged));

		/// <summary>
		///     Definition of the <see cref="XAxis" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty XAxisProperty =
			DependencyProperty.Register("XAxis", typeof (Axis), typeof (LineChart), new PropertyMetadata(default(Axis)));

		/// <summary>
		///     Definition of the <see cref="YAxis" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty YAxisProperty =
			DependencyProperty.Register("YAxis", typeof (Axis), typeof (LineChart), new PropertyMetadata(default(Axis)));

		private static readonly DependencyPropertyKey CanvasPropertyKey
			= DependencyProperty.RegisterReadOnly("Canvas", typeof (AbstractLineChartCanvas), typeof (LineChart),
			                                      new FrameworkPropertyMetadata(default(AbstractLineChartCanvas),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="Canvas" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty CanvasProperty
			= CanvasPropertyKey.DependencyProperty;

		private Grid _grid;

		static LineChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (LineChart), new FrameworkPropertyMetadata(typeof (LineChart)));
		}

		/// <summary>
		///     Initializes this <see cref="LineChart" />.
		/// </summary>
		public LineChart()
		{
			Canvas = new LineChartCanvas
				{
					Series = Series
				};
			Grid.SetColumn(Canvas, 1);
		}

		/// <summary>
		///     Definition of the y-axis.
		/// </summary>
		public Axis YAxis
		{
			get { return (Axis) GetValue(YAxisProperty); }
			set { SetValue(YAxisProperty, value); }
		}

		/// <summary>
		///     Definition of the x-axis.
		/// </summary>
		public Axis XAxis
		{
			get { return (Axis) GetValue(XAxisProperty); }
			set { SetValue(XAxisProperty, value); }
		}

		/// <summary>
		///     Defines how the <see cref="Series" /> should be displayed.
		/// </summary>
		public LineChartType ChartType
		{
			get { return (LineChartType) GetValue(ChartTypeProperty); }
			set { SetValue(ChartTypeProperty, value); }
		}

		/// <summary>
		///     The canvas used to actually draw the series.
		///     Used for testing.
		/// </summary>
		public AbstractLineChartCanvas Canvas
		{
			get { return (AbstractLineChartCanvas) GetValue(CanvasProperty); }
			protected set { SetValue(CanvasPropertyKey, value); }
		}

		/// <summary>
		/// </summary>
		public IEnumerable<ILineSeries> Series
		{
			get { return (IEnumerable<ILineSeries>) GetValue(SeriesProperty); }
			set { SetValue(SeriesProperty, value); }
		}

		private static void OnChartTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((LineChart) d).OnChartTypeChanged((LineChartType) e.NewValue);
		}

		private void OnChartTypeChanged(LineChartType type)
		{
			if (Canvas != null)
			{
				// We're about to no longer use this canvas.
				// Thus we HAVE to force it remove itself from all events
				// of the series collection (and its values) or otherweise
				// we're gonna have a leak..
				Canvas.Series = null;
				RemoveCanvas(Canvas);
			}

			Canvas = CreateCanvas(type);

			if (Canvas != null)
			{
				Grid.SetColumn(Canvas, 1);
				Canvas.Series = Series;
				AddCanvas(Canvas);
			}
		}

		private void AddCanvas(AbstractLineChartCanvas canvas)
		{
			if (_grid != null)
			{
				_grid.Children.Add(canvas);
			}
		}

		private void RemoveCanvas(AbstractLineChartCanvas canvas)
		{
			if (_grid != null)
				_grid.Children.Remove(canvas);
		}

		private static AbstractLineChartCanvas CreateCanvas(LineChartType type)
		{
			switch (type)
			{
				case LineChartType.Normal:
					return new LineChartCanvas();
				case LineChartType.Stacked:
					return new StackedLineChartCanvas();
				default:
					return null;
			}
		}

		private static void OnSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((LineChart) d).OnSeriesChanged((IEnumerable<ILineSeries>) e.NewValue);
		}

		private void OnSeriesChanged(IEnumerable<ILineSeries> newValue)
		{
			Canvas.Series = newValue;
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			RemoveCanvas(Canvas);
			_grid = (Grid) GetTemplateChild("PART_MainGrid");
			AddCanvas(Canvas);
		}
	}
}