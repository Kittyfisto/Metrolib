using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
		public static readonly DependencyProperty SeriesProperty =
			DependencyProperty.Register("Series", typeof (IEnumerable<LineSeries>), typeof (LineChart),
			                            new PropertyMetadata(null, OnSeriesChanged));

		private readonly List<LineChartCanvas> _canvasses;
		private Grid _grid;

		static LineChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (LineChart), new FrameworkPropertyMetadata(typeof (LineChart)));
		}

		public LineChart()
		{
			_canvasses = new List<LineChartCanvas>();
		}

		public IEnumerable<LineSeries> Series
		{
			get { return (IEnumerable<LineSeries>) GetValue(SeriesProperty); }
			set { SetValue(SeriesProperty, value); }
		}

		private static void OnSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((LineChart) d).OnSeriesChanged((IEnumerable<LineSeries>) e.OldValue, (IEnumerable<LineSeries>) e.NewValue);
		}

		private void OnSeriesChanged(IEnumerable<LineSeries> oldValue, IEnumerable<LineSeries> newValue)
		{
			var notifiable = oldValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= OnSeriesChanged;
			}
			notifiable = newValue as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged += OnSeriesChanged;
			}

			Clear();

			if (newValue != null)
			{
				foreach (var series in newValue)
				{
					AddCanvas(series);
				}
			}
		}

		private void Clear()
		{
			foreach (var canvas in _canvasses)
			{
				_grid.Children.Remove(canvas);
			}
			_canvasses.Clear();
		}

		private void AddCanvas(LineSeries series)
		{
			var canvas = new LineChartCanvas
			{
				Series = series
			};
			Grid.SetColumn(canvas, 1);
			_canvasses.Add(canvas);
			if (_grid != null)
			{
				_grid.Children.Add(canvas);
			}
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_grid = (Grid) GetTemplateChild("PART_MainGrid");
			if (_grid != null)
			{
				foreach (var canvas in _canvasses)
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
					foreach (LineSeries series in args.NewItems)
					{
						AddCanvas(series);
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					foreach (LineSeries series in args.OldItems)
					{
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

		private void RemoveCanvas(LineSeries series)
		{
			var canvas = _canvasses.FirstOrDefault(x => ReferenceEquals(x.Series, series));
			if (canvas != null)
			{
				_grid.Children.Remove(canvas);
				_canvasses.Remove(canvas);
			}
		}
	}
}