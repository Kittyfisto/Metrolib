using System;
using System.Collections.Generic;
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
		: INotifyPropertyChanged
	{
		private int _count;
		private Brush _fill;
		private IEnumerable<Point> _values;
		private Range _xRange;
		private Range _yRange;
		private Pen _outline;

		/// <summary>
		/// 
		/// </summary>
		public Pen Outline
		{
			get { return _outline; }
			set
			{
				if (value == _outline)
					return;

				_outline = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public Brush Fill
		{
			get { return _fill; }
			set
			{
				if (value == _fill)
					return;
				_fill = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		/// </summary>
		public IEnumerable<Point> Values
		{
			get { return _values; }
			set
			{
				if (value == _values)
					return;

				_values = value;
				EmitPropertyChanged();

				UpdateRanges();
			}
		}

		/// <summary>
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

		public event PropertyChangedEventHandler PropertyChanged;

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

				if (xRange.Maximum > xRange.Minimum)
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

		public List<Point> ProjectToView(double width, double height)
		{
			var ret = new List<Point>(_count);
			if (Values != null)
			{
				foreach (Point point in Values)
				{
					Range xRange = XRange;
					Range yRange = YRange;

					double x = xRange.GetRelative(point.X);
					double y = yRange.GetRelative(point.Y);

					var view = new Point(
						x*width,
						height*(1 - y)
						);
					ret.Add(view);
				}
			}
			return ret;
		}

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}