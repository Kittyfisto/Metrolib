using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Metrolib.Controls.Charts.Line;

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
		/// </summary>
		public static readonly DependencyProperty SeriesProperty =
			DependencyProperty.Register("Series", typeof (IEnumerable<ILineSeries>), typeof (LineChart),
			                            new PropertyMetadata(null, OnSeriesChanged));

		private static readonly DependencyPropertyKey XRangePropertyKey
			= DependencyProperty.RegisterReadOnly("XRange", typeof (Range), typeof (LineChart),
			                                      new FrameworkPropertyMetadata(default(Range),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty XRangeProperty
			= XRangePropertyKey.DependencyProperty;

		private static readonly DependencyPropertyKey YRangePropertyKey
			= DependencyProperty.RegisterReadOnly("YRange", typeof (Range), typeof (LineChart),
			                                      new FrameworkPropertyMetadata(default(Range),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty YRangeProperty
			= YRangePropertyKey.DependencyProperty;

		public static readonly DependencyProperty YAxisCaptionProperty =
			DependencyProperty.Register("YAxisCaption", typeof (object), typeof (LineChart),
										new PropertyMetadata(default(object)));

		public static readonly DependencyProperty XAxisCaptionProperty =
			DependencyProperty.Register("XAxisCaption", typeof(object), typeof(LineChart),
										new PropertyMetadata(default(object)));

		private readonly List<LineChartCanvas> _canvasses;
		private Grid _grid;

		static LineChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (LineChart), new FrameworkPropertyMetadata(typeof (LineChart)));
		}

		/// <summary>
		/// </summary>
		public LineChart()
		{
			_canvasses = new List<LineChartCanvas>();
		}

		/// <summary>
		///     Caption of the x-axis.
		/// </summary>
		public object XAxisCaption
		{
			get { return GetValue(XAxisCaptionProperty); }
			set { SetValue(XAxisCaptionProperty, value); }
		}

		/// <summary>
		///     Caption of the y-axis.
		/// </summary>
		public object YAxisCaption
		{
			get { return GetValue(YAxisCaptionProperty); }
			set { SetValue(YAxisCaptionProperty, value); }
		}

		/// <summary>
		/// </summary>
		public Range YRange
		{
			get { return (Range) GetValue(YRangeProperty); }
			protected set { SetValue(YRangePropertyKey, value); }
		}

		/// <summary>
		/// </summary>
		public Range XRange
		{
			get { return (Range) GetValue(XRangeProperty); }
			protected set { SetValue(XRangePropertyKey, value); }
		}

		/// <summary>
		/// </summary>
		public IEnumerable<ILineSeries> Series
		{
			get { return (IEnumerable<ILineSeries>) GetValue(SeriesProperty); }
			set { SetValue(SeriesProperty, value); }
		}

		private static void OnSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((LineChart) d).OnSeriesChanged((IEnumerable<ILineSeries>) e.OldValue, (IEnumerable<ILineSeries>) e.NewValue);
		}

		private void OnSeriesChanged(IEnumerable<ILineSeries> oldValue, IEnumerable<ILineSeries> newValue)
		{
			var notifiable = oldValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= OnSeriesChanged;
			}
			if (oldValue != null)
			{
				foreach (ILineSeries series in oldValue)
				{
					series.PropertyChanged -= SeriesOnPropertyChanged;
				}
			}

			notifiable = newValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged += OnSeriesChanged;
			}
			if (newValue != null)
			{
				foreach (ILineSeries series in newValue)
				{
					series.PropertyChanged += SeriesOnPropertyChanged;
				}
			}

			Clear();

			if (newValue != null)
			{
				foreach (ILineSeries series in newValue)
				{
					AddCanvas(series);
				}
			}

			UpdateRanges();
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_grid = (Grid) GetTemplateChild("PART_MainGrid");
			if (_grid != null)
			{
				foreach (LineChartCanvas canvas in _canvasses)
				{
					_grid.Children.Add(canvas);
				}
			}
		}

		private void OnSeriesChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					foreach (ILineSeries series in args.NewItems)
					{
						series.PropertyChanged -= SeriesOnPropertyChanged;
						AddCanvas(series);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (ILineSeries series in args.OldItems)
					{
						series.PropertyChanged += SeriesOnPropertyChanged;
						RemoveCanvas(series);
					}
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Reset:
					Clear();
					break;
			}
		}

		private void Clear()
		{
			if (_grid != null)
			{
				foreach (LineChartCanvas canvas in _canvasses)
				{
					_grid.Children.Remove(canvas);
				}
			}
			_canvasses.Clear();
		}

		private void AddCanvas(ILineSeries series)
		{
			var canvas = new LineChartCanvas
				{
					Series = series,
					ClipToBounds = true
				};

			BindingOperations.SetBinding(canvas, LineChartCanvas.XRangeProperty, new Binding("XRange")
				{
					Source = this
				});
			BindingOperations.SetBinding(canvas, LineChartCanvas.YRangeProperty, new Binding("YRange")
				{
					Source = this
				});

			Grid.SetColumn(canvas, 1);
			_canvasses.Add(canvas);
			if (_grid != null)
			{
				_grid.Children.Add(canvas);
			}
		}

		private void RemoveCanvas(ILineSeries series)
		{
			LineChartCanvas canvas = _canvasses.FirstOrDefault(x => ReferenceEquals(x.Series, series));
			if (canvas != null)
			{
				if (_grid != null)
				{
					_grid.Children.Remove(canvas);
				}
				_canvasses.Remove(canvas);
			}
		}

		private void SeriesOnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			switch (args.PropertyName)
			{
				case "XRange":
				case "YRange":
					UpdateRanges();
					break;
			}
		}

		private void UpdateRanges()
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
	}
}