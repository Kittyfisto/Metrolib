using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Describes the x- or y-axis of a <see cref="LineChart" />.
	/// </summary>
	public sealed class Axis
		: INotifyPropertyChanged
	{
		private object _caption;
		private Pen _linePen;
		private bool _showLines;
		private bool _showTicks;
		private double _spacing;

		/// <summary>
		///     Initializes this axis.
		/// </summary>
		public Axis()
		{
			_spacing = 100;
			_showLines = true;
			_showTicks = true;
			_linePen = new Pen(new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)), 1);
		}

		/// <summary>
		///     The pen used to draw lines over the diagram.
		/// </summary>
		/// <remarks>
		///     Lines are only drawn when there is enough space (<see cref="Spacing" />) and when
		///     <see cref="ShowLines" /> is true.
		/// </remarks>
		public Pen LinePen
		{
			get { return _linePen; }
			set
			{
				if (ReferenceEquals(value, _linePen))
					return;

				_linePen = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The spacing between ticks / lines in device independent units.
		/// </summary>
		public double Spacing
		{
			get { return _spacing; }
			set
			{
				if (value == _spacing)
					return;

				_spacing = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The caption next to the axis.
		///     Will be presented by a <see cref="ContentPresenter" />.
		/// </summary>
		/// <remarks>
		///     The content of the y-axis is rotated by 90° counter clockwise.
		/// </remarks>
		public object Caption
		{
			get { return _caption; }
			set
			{
				if (value == _caption)
					return;

				_caption = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		/// </summary>
		public bool ShowTicks
		{
			get { return _showTicks; }
			set
			{
				if (value == _showTicks)
					return;

				_showTicks = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		/// </summary>
		public bool ShowLines
		{
			get { return _showLines; }
			set
			{
				if (value == _showLines)
					return;

				_showLines = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     Is fired whenever a property changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// </summary>
		/// <param name="valueRange"></param>
		/// <param name="display"></param>
		/// <returns></returns>
		public IEnumerable<double> GetLines(Range valueRange, double display)
		{
			var displayRange = new Range(0, display);
			var ret = new List<double>();
			double value = _spacing;
			while (value < displayRange.Maximum)
			{
				ret.Add(value);
				value += _spacing;
			}
			return ret;
		}
	}
}