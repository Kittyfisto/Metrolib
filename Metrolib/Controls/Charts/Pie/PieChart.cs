using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public class PieChart
		: Canvas
	{
		/// <summary>
		///     Definition of the <see cref="Series" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SeriesProperty =
			DependencyProperty.Register("Series", typeof (IPieSeries), typeof (PieChart),
			                            new PropertyMetadata(null, OnSeriesChanged));

		/// <summary>
		///     Definition of the <see cref="TitleTemplate" />
		/// </summary>
		public static readonly DependencyProperty TitleTemplateProperty =
			DependencyProperty.Register("TitleTemplate", typeof (DataTemplate), typeof (PieChart),
			                            new PropertyMetadata(default(DataTemplate)));

		private static readonly DependencyPropertyKey SumOfSlicesPropertyKey
			= DependencyProperty.RegisterReadOnly("SumOfSlices", typeof (double), typeof (PieChart),
			                                      new FrameworkPropertyMetadata(default(double),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="Outline" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OutlineProperty =
			DependencyProperty.Register("Outline", typeof (Pen), typeof (PieChart), new PropertyMetadata(default(Pen)));

		/// <summary>
		///     Definition of the <see cref="SumOfSlices" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SumOfSlicesProperty
			= SumOfSlicesPropertyKey.DependencyProperty;

		private readonly Dictionary<IPieSlice, PieChartSliceItem> _sliceItems;
		private readonly Dictionary<IPieSlice, PieChartTitleItem> _titleItems;

		private bool _isLoaded;
		private IEnumerable<IPieSlice> _slices;

		static PieChart()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (PieChart), new FrameworkPropertyMetadata(typeof (PieChart)));
		}

		/// <summary>
		///     Initializes this chart.
		/// </summary>
		public PieChart()
		{
			_sliceItems = new Dictionary<IPieSlice, PieChartSliceItem>();
			_titleItems = new Dictionary<IPieSlice, PieChartTitleItem>();

			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
			SizeChanged += OnSizeChanged;
		}

		/// <summary>
		///     The data template, if any, used to present a <see cref="IPieSlice.Title" />.
		/// </summary>
		public DataTemplate TitleTemplate
		{
			get { return (DataTemplate) GetValue(TitleTemplateProperty); }
			set { SetValue(TitleTemplateProperty, value); }
		}

		/// <summary>
		///     The outline used to draw *around* the entire bar chart.
		/// </summary>
		public Pen Outline
		{
			get { return (Pen) GetValue(OutlineProperty); }
			set { SetValue(OutlineProperty, value); }
		}

		/// <summary>
		///     The sum of the individual values of all slices.
		/// </summary>
		public double SumOfSlices
		{
			get { return (double) GetValue(SumOfSlicesProperty); }
			protected set { SetValue(SumOfSlicesPropertyKey, value); }
		}

		/// <summary>
		///     The series displayed by this chart.
		/// </summary>
		public IPieSeries Series
		{
			get { return (IPieSeries) GetValue(SeriesProperty); }
			set { SetValue(SeriesProperty, value); }
		}

		private void OnSizeChanged(object sender, SizeChangedEventArgs args)
		{
			MakeDirty();
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			if (_isLoaded)
				return;

			SubscribeTo(Series);
			MakeDirty();

			_isLoaded = true;
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			UnsubscribeFrom(Series);
			_isLoaded = false;
		}

		private static void OnSeriesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PieChart) d).OnSeriesChanged((IPieSeries) e.OldValue, (IPieSeries) e.NewValue);
		}

		private void OnSeriesChanged(IPieSeries oldValue, IPieSeries newValue)
		{
			if (_isLoaded)
			{
				UnsubscribeFrom(oldValue);
				SubscribeTo(newValue);
			}
		}

		private void ClearItems()
		{
			foreach (var item in _sliceItems.Values)
			{
				InternalChildren.Remove(item);
			}
			_sliceItems.Clear();

			foreach (var item in _titleItems.Values)
			{
				InternalChildren.Remove(item);
			}
			_titleItems.Clear();
		}

		private void SubscribeTo(IPieSeries series)
		{
			var notifiable = series as INotifyPropertyChanged;
			if (notifiable != null)
			{
				notifiable.PropertyChanged += SeriesOnPropertyChanged;
			}

			if (series != null)
			{
				SubscribeTo(series.Slices);
				AddItems(series.Slices);
				_slices = series.Slices;
				SumOfSlices = _slices.Sum(x => x.Value);
			}
			else
			{
				_slices = null;
				SumOfSlices = 0;
			}
		}

		private void AddItems(IEnumerable<IPieSlice> slices)
		{
			if (slices == null)
				return;

			foreach (IPieSlice slice in slices)
			{
				OnSliceAdded(slice);
			}
		}

		private void OnSliceAdded(IPieSlice slice)
		{
			var titleItem = new PieChartTitleItem
				{
					Content = slice.Title
				};
			_titleItems.Add(slice, titleItem);
			InternalChildren.Add(titleItem);

			var sliceItem = new PieChartSliceItem
				{
					Slice = slice
				};
			_sliceItems.Add(slice, sliceItem);
			InternalChildren.Add(sliceItem);
		}

		private void SeriesOnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			switch (args.PropertyName)
			{
				case "Slices":
					UnsubscribeFrom(_slices);
					SubscribeTo(Series.Slices);
					_slices = Series.Slices;
					break;
			}
		}

		private void UnsubscribeFrom(IPieSeries series)
		{
			var notifiable = series as INotifyPropertyChanged;
			if (notifiable != null)
			{
				notifiable.PropertyChanged -= SeriesOnPropertyChanged;
			}

			UnsubscribeFrom(series.Slices);
			ClearItems();
		}

		private void SubscribeTo(IEnumerable<IPieSlice> slices)
		{
			var notifiable = slices as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged += SlicesOnCollectionChanged;
			}
		}

		private void SlicesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			MakeDirty();
		}

		private void MakeDirty()
		{
			InvalidateVisual();
		}

		private void UnsubscribeFrom(IEnumerable<IPieSlice> slices)
		{
			var notifiable = slices as INotifyCollectionChanged;
			if (notifiable != null)
			{
				notifiable.CollectionChanged -= SlicesOnCollectionChanged;
			}
		}

		/// <summary>
		/// Arranges the content of a System.Windows.Controls.Canvas element.
		/// </summary>
		/// <param name="arrangeSize"></param>
		/// <returns></returns>
		protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
		{
			if (_slices != null)
			{
				double radius = Math.Min(ActualWidth / 2, ActualHeight / 2);
				double startAngle = 0;

				foreach (var slice in _slices)
				{
					var item = _sliceItems[slice];

					double relativeValue = item.Slice.Value / SumOfSlices;
					double openAngle = relativeValue * 2 * Math.PI;

					item.StartAngle = startAngle;
					item.OpenAngle = openAngle;
					item.Radius = radius;

					startAngle += openAngle;
				}
			}

			var offset = new Vector(ActualWidth / 2, ActualHeight / 2);
			foreach (var pair in _titleItems)
			{
				var slice = pair.Key;
				var item = pair.Value;
				var sliceItem = _sliceItems[slice];

				var position = GetPoint(sliceItem.Radius, sliceItem.StartAngle + sliceItem.OpenAngle/2) + offset;
				var rect = new Rect(position, item.DesiredSize);
				item.Arrange(rect);
			}

			return arrangeSize;
		}

		/// <summary>
		/// Draws the content of a System.Windows.Media.DrawingContext object during the render pass of a System.Windows.Controls.Panel element.
		/// </summary>
		/// <param name="drawingContext"></param>
		protected override void OnRender(DrawingContext drawingContext)
		{
			double radius = Math.Min(ActualWidth/2, ActualHeight/2);
			var offset = new Vector(ActualWidth/2, ActualHeight/2);
			var center = new Point(0, 0);

			if (_slices != null)
			{
				drawingContext.PushTransform(new TranslateTransform(offset.X, offset.Y));
				try
				{
					foreach (var item in _sliceItems.Values)
					{
						double startAngle = item.StartAngle;
						double openAngle = item.OpenAngle;
						var geometry = new StreamGeometry();
						bool isStroked = item.Slice.Outline != null;

						using (StreamGeometryContext context = geometry.Open())
						{
							context.BeginFigure(center, true, true);
							context.LineTo(GetPoint(radius, startAngle), isStroked, false);
							context.ArcTo(GetPoint(radius, startAngle + openAngle),
										  new System.Windows.Size(radius, radius),
										  startAngle * 180 / Math.PI,
										  openAngle > Math.PI,
										  SweepDirection.Counterclockwise,
										  isStroked,
										  false);
							context.LineTo(center, isStroked, false);
						}

						drawingContext.DrawGeometry(item.Slice.Fill, item.Slice.Outline, geometry);
					}
				}
				finally
				{
					drawingContext.Pop();
				}
			}
		}

		private static Point GetPoint(double radius, double angle)
		{
			return new Point(Math.Sin(angle)*radius,
			                 Math.Cos(angle)*radius);
		}
	}
}