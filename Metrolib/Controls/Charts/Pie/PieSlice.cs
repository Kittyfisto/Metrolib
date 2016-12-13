using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The default implementation of a <see cref="IPieSlice" />.
	/// </summary>
	public sealed class PieSlice
		: IPieSlice
		  , INotifyPropertyChanged
	{
		private Brush _fill;
		private Pen _outline;

		private object _label;
		private object _tooltip;
		private double _value;
		private object _displayedValue;

		/// <summary>
		///     Initializes this slice.
		/// </summary>
		public PieSlice()
		{
			Outline = new Pen(Brushes.Black, 1);
			Outline.Freeze();

			Fill = Brushes.DodgerBlue;
		}

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		///     The pen used to draw the outline of the slice, if any.
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
		/// The brush used to fill the area of the slice, if any.
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
		///     The value of this slice.
		/// </summary>
		public double Value
		{
			get { return _value; }
			set
			{
				if (value == _value)
					return;

				_value = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The value of this slice, as it shall be displayed.
		/// </summary>
		public object DisplayedValue
		{
			get { return _displayedValue; }
			set
			{
				if (value == _displayedValue)
					return;

				_displayedValue = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The title of this slice, if any.
		/// </summary>
		public object Label
		{
			get { return _label; }
			set
			{
				if (value == _label)
					return;

				_label = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		///     The tooltip of this slice, if any.
		/// </summary>
		public object Tooltip
		{
			get { return _tooltip; }
			set
			{
				if (value == _tooltip)
					return;

				_tooltip = value;
				EmitPropertyChanged();
			}
		}

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0}: {1}", Label, Value);
		}

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}