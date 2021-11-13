using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Metrolib.Controls.Charts.Line.Canvas
{
	/// <summary>
	///     Responsible for drawing one or more <see cref="ILineSeries" />.
	/// </summary>
	public abstract class AbstractLineChartCanvas
		: Control
	{
		/// <summary>
		///     The minimum time we try to run our updates in.
		///     We will however reduce this in integer increments in case our update method takes too long (so
		///     we do not cause a buildup of events).
		/// </summary>
		public static TimeSpan MinimumUpdateDelta = TimeSpan.FromMilliseconds(1/60.0);

		/// <summary>
		///     Definition of the <see cref="XAxis" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty XAxisProperty =
			DependencyProperty.Register("XAxis", typeof(AxisDefinition), typeof(AbstractLineChartCanvas), new PropertyMetadata(default(AxisDefinition)));

		/// <summary>
		///     Definition of the <see cref="YAxis" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty YAxisProperty =
			DependencyProperty.Register("YAxis", typeof(AxisDefinition), typeof(AbstractLineChartCanvas), new PropertyMetadata(default(AxisDefinition)));

		/// <summary>
		///     Definition of the <see cref="XRange" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty XRangeProperty =
			DependencyProperty.Register("XRange", typeof (Range), typeof (AbstractLineChartCanvas),
			                            new PropertyMetadata(default(Range)));

		/// <summary>
		///     Definition of the <see cref="YRange" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty YRangeProperty =
			DependencyProperty.Register("YRange", typeof (Range), typeof (AbstractLineChartCanvas),
			                            new PropertyMetadata(default(Range)));

		private readonly List<AbstractLineSeriesCanvas> _seriesCanvasses;

		private bool _isDirty;
		private IEnumerable<ILineSeries> _series;

		/// <summary>
		///     Initializes this canvas.
		/// </summary>
		protected AbstractLineChartCanvas()
		{
			_seriesCanvasses = new List<AbstractLineSeriesCanvas>();
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
			SizeChanged += OnSizeChanged;

			// We draw stuff by hand and thus we must enable clipping to ensure
			// that we don't draw outside of our client area.
			ClipToBounds = true;
		}

		/// <summary>
		/// </summary>
		public AxisDefinition YAxis
		{
			get { return (AxisDefinition)GetValue(YAxisProperty); }
			set { SetValue(YAxisProperty, value); }
		}

		/// <summary>
		/// </summary>
		public AxisDefinition XAxis
		{
			get { return (AxisDefinition)GetValue(XAxisProperty); }
			set { SetValue(XAxisProperty, value); }
		}

		/// <summary>
		///     The computed range of x-values in all <see cref="Series" />.
		/// </summary>
		public Range XRange
		{
			get { return (Range) GetValue(XRangeProperty); }
			set { SetValue(XRangeProperty, value); }
		}

		/// <summary>
		///     The computed range of y-values in all <see cref="Series" />.
		/// </summary>
		public Range YRange
		{
			get { return (Range) GetValue(YRangeProperty); }
			set { SetValue(YRangeProperty, value); }
		}

		/// <summary>
		///     The series to display.
		/// </summary>
		public IEnumerable<ILineSeries> Series
		{
			get { return _series; }
			set
			{
				if (ReferenceEquals(value, _series))
					return;

				ClearSeries();
				if (_series != null)
				{
					var notifiable = _series as INotifyCollectionChanged;
					if (notifiable != null)
						notifiable.CollectionChanged -= SeriesOnCollectionChanged;
				}

				_series = value;

				if (_series != null)
				{
					var notifiable = _series as INotifyCollectionChanged;
					if (notifiable != null)
						notifiable.CollectionChanged += SeriesOnCollectionChanged;
					AddSeries(0, _series);
				}
			}
		}

		/// <summary>
		///     The list of canvasses, one per <see cref="Series" />.
		/// </summary>
		public IEnumerable<AbstractLineSeriesCanvas> SeriesCanvasses
		{
			get { return _seriesCanvasses; }
		}

		/// <summary>
		///     Whether or not this canvas is dirty and must be updated and rendered.
		/// </summary>
		public bool IsDirty
		{
			get { return _isDirty; }
		}

		/// <summary>
		///     Marks this canvas as dirty so it actually does some work the next time <see cref="Update" /> is called.
		/// </summary>
		protected void SetDirty()
		{
			_isDirty = true;
		}

		/// <summary>
		///     Is called to prepare the canvas for rendering.
		/// </summary>
		public virtual void Update()
		{
			CalculateCombinedRanges();

			bool redraw = IsDirty;
			foreach (AbstractLineSeriesCanvas canvas in SeriesCanvasses)
			{
				canvas.DisplayedXRange = XRange;
				canvas.DisplayedYRange = YRange;

				if (canvas.Update())
					redraw = true;
			}

			if (redraw)
			{
				InvalidateVisual();
			}
		}

		/// <summary>
		///     Is called to determine the range of all series.
		/// </summary>
		private void CalculateCombinedRanges()
		{
			if (Series != null)
			{
				IEnumerator<AbstractLineSeriesCanvas> it = SeriesCanvasses.GetEnumerator();
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

		private void OnSizeChanged(object sender, SizeChangedEventArgs args)
		{
			foreach (AbstractLineSeriesCanvas canvas in _seriesCanvasses)
			{
				canvas.Width = args.NewSize.Width;
				canvas.Height = args.NewSize.Height;
			}
		}

		/// <summary>
		///     Is called to render the contents of this canvas.
		/// </summary>
		/// <param name="drawingContext"></param>
		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);

			foreach (AbstractLineSeriesCanvas canvas in _seriesCanvasses)
			{
				canvas.OnRender(drawingContext);
			}

			if (XAxis != null && XAxis.LinePen != null)
			{
				foreach (var value in XAxis.GetLines(XRange, ActualWidth))
				{
					var from = new Point(value, 0);
					var to = new Point(value, ActualHeight);
					drawingContext.DrawLine(XAxis.LinePen, from, to);
				}
			}

			if (YAxis != null && YAxis.LinePen != null)
			{
				foreach (var value in YAxis.GetLines(YRange, ActualHeight))
				{
					var from = new Point(0, ActualHeight - value);
					var to = new Point(ActualWidth, ActualHeight - value);
					drawingContext.DrawLine(YAxis.LinePen, from, to);
				}
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			// TODO: We should connect to events here in case Series is non-null.
			//       Also, we should NOT connect to events when the series is changed
			//       while this control is not loaded.
		}

		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			// We definately should disconnect from events from viewmodels when we're
			// unloaded because these models might live longer than this control (because
			// it might be dynamically added / removed from the visual tree).
			var notifiable = _series as INotifyCollectionChanged;
			if (notifiable != null)
				notifiable.CollectionChanged -= SeriesOnCollectionChanged;
		}

		/// <summary>
		///     Creates a canvas responsible for drawing the given series only.
		/// </summary>
		/// <param name="series"></param>
		/// <returns></returns>
		protected abstract AbstractLineSeriesCanvas CreateCanvas(ILineSeries series);

		private void AddSeries(int startingIndex, IEnumerable<ILineSeries> series)
		{
			int i = startingIndex;
			foreach (ILineSeries s in series)
			{
				AddSeries(i, s);
				++i;
			}
		}

		private void AddSeries(int index, ILineSeries series)
		{
			AbstractLineSeriesCanvas canvas = CreateCanvas(series);
			canvas.Width = ActualWidth;
			canvas.Height = ActualHeight;
			_seriesCanvasses.Insert(index, canvas);
		}

		private void RemoveSeries(IEnumerable<ILineSeries> series)
		{
			foreach (ILineSeries s in series)
			{
				RemoveSeries(s);
			}
		}

		private void RemoveSeries(ILineSeries series)
		{
			AbstractLineSeriesCanvas canvas = _seriesCanvasses.FirstOrDefault(x => x.Series == series);
			if (canvas != null)
			{
				_seriesCanvasses.Remove(canvas);
			}
		}

		private void ClearSeries()
		{
			if (_series != null)
			{
				foreach (AbstractLineSeriesCanvas canvas in _seriesCanvasses)
				{
					canvas.Dispose();
				}
				_seriesCanvasses.Clear();
			}
		}

		private void SeriesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddSeries(args.NewStartingIndex, args.NewItems.OfType<ILineSeries>());
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Remove:
					RemoveSeries(args.OldItems.OfType<ILineSeries>());
					break;
				case NotifyCollectionChangedAction.Replace:
					RemoveSeries(args.OldItems.OfType<ILineSeries>());
					AddSeries(args.NewStartingIndex, args.NewItems.OfType<ILineSeries>());
					break;
				case NotifyCollectionChangedAction.Reset:
					ClearSeries();
					AddSeries(0, args.NewItems.OfType<ILineSeries>());
					break;
			}
		}
	}
}