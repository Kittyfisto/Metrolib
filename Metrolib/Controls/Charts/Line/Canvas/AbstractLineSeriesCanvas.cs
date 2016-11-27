using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Windows;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Responsible for drawing an individual <see cref="ILineSeries" />.
	/// </summary>
	public abstract class AbstractLineSeriesCanvas
		: IDisposable
	{
		private readonly ILineSeries _lineSeries;

		private bool _isDirty;
		private INotifyCollectionChanged _observableValues;
		private Point[] _values;
		private Range _xRange;
		private Range _yRange;
		private double _width;
		private double _height;
		private bool _isValuesDirty;

		protected AbstractLineSeriesCanvas(ILineSeries lineSeries)
		{
			_lineSeries = lineSeries;
			_lineSeries.PropertyChanged += LineSeriesOnPropertyChanged;

			OnValuesChanged(_lineSeries.Values);
			MakeDirty();
		}

		/// <summary>
		/// The values this canvas should display.
		/// </summary>
		protected IEnumerable<Point> Values
		{
			get { return _values; }
		}

		/// <summary>
		///     The range of x-values that shall be displayed by this canvas.
		/// </summary>
		public Range XRange
		{
			get { return _xRange; }
			set
			{
				if (value == _xRange)
					return;

				_xRange = value;
				MakeDirty();
			}
		}

		/// <summary>
		///     The range of y-values that shall be displayed by this canvas.
		/// </summary>
		public Range YRange
		{
			get { return _yRange; }
			set
			{
				if (value == _yRange)
					return;

				_yRange = value;
				MakeDirty();
			}
		}

		/// <summary>
		///     The width in independent device units that this canvas may use.
		/// </summary>
		public double Width
		{
			get { return _width; }
			set
			{
				if (value == _width)
					return;

				_width = value;
				MakeDirty();
			}
		}

		/// <summary>
		///     The height in independent device units that this canvas may use.
		/// </summary>
		public double Height
		{
			get { return _height; }
			set
			{
				if (value == _height)
					return;

				_height = value;
				MakeDirty();
			}
		}

		protected void MakeDirty()
		{
			_isDirty = true;
		}

		public ILineSeries Series
		{
			get { return _lineSeries; }
		}

		public void Dispose()
		{
			_lineSeries.PropertyChanged -= LineSeriesOnPropertyChanged;
		}

		[Pure]
		protected List<Point> ProjectToView(IEnumerable<Point> values, int count)
		{
			var ret = new List<Point>(count);
			if (values != null)
			{
				foreach (Point point in values)
				{
					double x = _xRange.GetRelative(point.X);
					double y = _yRange.GetRelative(point.Y);

					var view = new Point(
						x * _width,
						_height * (1 - y)
						);
					ret.Add(view);
				}
			}
			return ret;
		}

		private void OnValuesChanged(IEnumerable<Point> values)
		{
			if (_observableValues != null)
			{
				_observableValues.CollectionChanged -= OnValuesCollectionChanged;
			}

			_values = values != null ? values.ToArray() : new Point[0];
			_observableValues = values as INotifyCollectionChanged;

			if (_observableValues != null)
			{
				_observableValues.CollectionChanged += OnValuesCollectionChanged;
			}
		}

		private void OnValuesCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			_isValuesDirty = true;
		}

		/// <summary>
		///     Updates this collection, if necessary.
		/// </summary>
		/// <returns>true when something has changed so this series needs to be redrawn.</returns>
		public virtual bool Update()
		{
			if (_isValuesDirty)
			{
				_values = Series.Values.ToArray();
			}

			if (_isDirty)
			{
				_isDirty = false;
				return true;
			}

			return false;
		}

		/// <summary>
		///     This method is called to actually draw the <see cref="ILineSeries" /> represented by this canvas.
		/// </summary>
		/// <param name="drawingContext"></param>
		public abstract void OnRender(DrawingContext drawingContext);

		private void LineSeriesOnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			_isDirty = true;

			switch (args.PropertyName)
			{
				case "Values":
					OnValuesChanged(_lineSeries.Values);
					break;
			}
		}
	}
}