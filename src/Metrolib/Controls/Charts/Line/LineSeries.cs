using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public sealed class LineSeries
		: ILineSeries
	{
		private int _count;
		private Brush _fill;
		private Pen _outline;
		private Brush _pointFill;
		private Pen _pointOutline;
		private double _pointRadius;
		private IEnumerable<Point> _values;

		private Range _xRange;
		private Range _yRange;

		/// <summary>
		/// </summary>
		public LineSeries()
		{
			_outline = new Pen(Brushes.DodgerBlue, 2);
		}

		/// <summary>
		///     The amount of points in <see cref="Values" />.
		/// </summary>
		public int Count
		{
			get { return _count; }
		}

		/// <summary>
		///     Returns the value that should be displayed instead of the given numerical value.
		///     Is used to annotate axes and popups / tooltips.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public object GetXValue(double value)
		{
			return value;
		}

		/// <summary>
		///     Returns the value that should be displayed instead of the given numerical value.
		///     Is used to annotate axes and popups / tooltips.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public object GetYValue(double value)
		{
			return value;
		}

		/// <summary>
		///     The pen to draw the outline of this series with, if any.
		/// </summary>
		public Pen Outline
		{
			get { return _outline; }
			set
			{
				if (ReferenceEquals(value, _outline))
					return;

				_outline = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The brush to fill the area under this series, if any.
		/// </summary>
		public Brush Fill
		{
			get { return _fill; }
			set
			{
				if (ReferenceEquals(value, _fill))
					return;

				_fill = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The pen to draw the outline of the shape, representing individual data points.
		/// </summary>
		public Pen PointOutline
		{
			get { return _pointOutline; }
			set
			{
				if (ReferenceEquals(value, _pointOutline))
					return;

				_pointOutline = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The brush to fill the shape, representing individual data points.
		/// </summary>
		public Brush PointFill
		{
			get { return _pointFill; }
			set
			{
				if (ReferenceEquals(value, _pointFill))
					return;

				_pointFill = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The radius to draw the circle, representing individual data points.
		/// </summary>
		public double PointRadius
		{
			get { return _pointRadius; }
			set
			{
				if (value == _pointRadius)
					return;

				_pointRadius = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The values to display.
		/// </summary>
		/// <remarks>
		///     Values are assumed to be ordered ascending by their <see cref="Point.X" /> value.
		///     If this is not the case then the wrong data might be displayed.
		/// </remarks>
		public IEnumerable<Point> Values
		{
			get { return _values; }
			set
			{
				if (ReferenceEquals(value, _values))
					return;

				var notify = _values as INotifyCollectionChanged;
				if (notify != null)
					notify.CollectionChanged -= OnValuesChanged;

				_values = value;

				notify = Values as INotifyCollectionChanged;
				if (notify != null)
					notify.CollectionChanged += OnValuesChanged;

				EmitPropertyChanged();

				UpdateRanges();
			}
		}

		/// <summary>
		///     The minimum and maximum x values in <see cref="Values" />.
		/// </summary>
		public Range XRange
		{
			get { return _xRange; }
			private set
			{
				if (value == _xRange)
					return;

				_xRange = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The minimum and maximum y values in <see cref="Values" />.
		/// </summary>
		public Range YRange
		{
			get { return _yRange; }
			private set
			{
				if (value == _yRange)
					return;

				_yRange = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		private void OnValuesChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			UpdateRanges();
		}

		private void UpdateRanges()
		{
			_count = 0;
			if (Values != null)
			{
				var xRange = new Range(double.MaxValue, double.MinValue);
				var yRange = new Range(double.MaxValue, double.MinValue);
				foreach (Point point in Values)
				{
					xRange.Maximum = Math.Max(point.X, xRange.Maximum);
					xRange.Minimum = Math.Min(point.X, xRange.Minimum);

					yRange.Maximum = Math.Max(point.Y, yRange.Maximum);
					yRange.Minimum = Math.Min(point.Y, yRange.Minimum);

					++_count;
				}

				if (xRange.Maximum >= xRange.Minimum)
				{
					XRange = xRange;
					YRange = yRange;
				}
				else
				{
					XRange = new Range();
					YRange = new Range();
				}

				XRange = XRange;
				YRange = YRange;
			}
			else
			{
				XRange = new Range();
				YRange = new Range();
			}
		}

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}