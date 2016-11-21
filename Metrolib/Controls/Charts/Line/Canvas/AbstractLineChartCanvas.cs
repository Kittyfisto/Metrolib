using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using log4net;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Responsible for drawing one or more <see cref="ILineSeries" />.
	/// </summary>
	public abstract class AbstractLineChartCanvas
		: Control
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		///     The minimum time we try to run our updates in.
		///     We will however reduce this in integer increments in case our update method takes too long (so
		///     we do not cause a buildup of events).
		/// </summary>
		public static TimeSpan MinimumUpdateDelta = TimeSpan.FromMilliseconds(1/60.0);

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

		private readonly List<AbstractLineSeriesCanvas> _seriesCanvas;
		private readonly Stopwatch _stopwatch;
		private readonly DispatcherTimer _timer;

		private bool _isDirty;
		private IEnumerable<ILineSeries> _series;

		protected AbstractLineChartCanvas()
		{
			_stopwatch = new Stopwatch();
			_seriesCanvas = new List<AbstractLineSeriesCanvas>();
			_timer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMilliseconds(66)
				};
			_timer.Tick += TimerOnTick;
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;

			// We draw stuff by hand and thus we must enable clipping to ensure
			// that we don't draw outside of our client area.
			ClipToBounds = true;
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
				if (value == _series)
					return;

				ClearSeries();

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

		protected void SetDirty()
		{
			_isDirty = true;
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			_stopwatch.Restart();
			try
			{
				Update();
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Caught unexpected exception: {0}", e);
			}
			finally
			{
				_stopwatch.Stop();
			}

			if (Log.IsDebugEnabled)
			{
				Log.DebugFormat("Updating line chart took {0}ms", _stopwatch.ElapsedMilliseconds);
			}

			if (_stopwatch.Elapsed > MinimumUpdateDelta)
			{
				Log.WarnFormat(
					"Updating line chart took longer than {0}ms, if this happens frequently, please reduce the amount of data displayed or contact the author on github",
					MinimumUpdateDelta);
			}
		}

		internal void Update()
		{
			bool redraw = _isDirty;
			foreach (AbstractLineSeriesCanvas canvas in _seriesCanvas)
			{
				if (canvas.Update())
					redraw = true;
			}

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

			if (redraw)
			{
				InvalidateVisual();
			}
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			base.OnRender(drawingContext);

			foreach (AbstractLineSeriesCanvas canvas in _seriesCanvas)
			{
				canvas.OnRender(drawingContext, XRange, YRange, ActualWidth, ActualHeight);
			}
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_timer.Start();
		}

		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			_timer.Stop();
		}

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
			_seriesCanvas.Insert(index, CreateCanvas(series));
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
			AbstractLineSeriesCanvas canvas = _seriesCanvas.FirstOrDefault(x => x.Series == series);
			if (canvas != null)
			{
				_seriesCanvas.Remove(canvas);
			}
		}

		private void ClearSeries()
		{
			if (_series != null)
			{
				var notifiable = _series as INotifyCollectionChanged;
				if (notifiable != null)
					notifiable.CollectionChanged -= SeriesOnCollectionChanged;

				foreach (AbstractLineSeriesCanvas canvas in _seriesCanvas)
				{
					canvas.Dispose();
				}
				_seriesCanvas.Clear();
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