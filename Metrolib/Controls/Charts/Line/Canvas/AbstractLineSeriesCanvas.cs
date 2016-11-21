using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
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
		private IEnumerable<Point> _values;

		public IEnumerable<Point> Values
		{
			get { return _values; }
		}

		protected AbstractLineSeriesCanvas(ILineSeries lineSeries)
		{
			_lineSeries = lineSeries;
			_lineSeries.PropertyChanged += LineSeriesOnPropertyChanged;

			OnValuesChanged(_lineSeries.Values);
		}

		public ILineSeries Series
		{
			get { return _lineSeries; }
		}

		public void Dispose()
		{
			_lineSeries.PropertyChanged -= LineSeriesOnPropertyChanged;
		}

		private void OnValuesChanged(IEnumerable<Point> values)
		{
			if (_observableValues != null)
			{
				_observableValues.CollectionChanged -= OnValuesCollectionChanged;
			}

			_values = values;
			_observableValues = values as INotifyCollectionChanged;

			if (_observableValues != null)
			{
				_observableValues.CollectionChanged += OnValuesCollectionChanged;
			}
		}

		private void OnValuesCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			_isDirty = true;
		}

		/// <summary>
		///     Updates this collection, if necessary.
		/// </summary>
		/// <returns>true when something has changed so this series needs to be redrawn.</returns>
		public bool Update()
		{
			if (_isDirty)
			{
				_isDirty = false;
				return true;
			}

			return false;
		}

		public abstract void OnRender(DrawingContext drawingContext,
		                              Range xRange,
		                              Range yRange,
		                              double width,
		                              double height);

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