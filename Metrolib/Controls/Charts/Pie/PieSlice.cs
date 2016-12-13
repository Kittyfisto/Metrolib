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

		private object _title;
		private object _tooltip;
		private double _value;

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
		///     The title of this slice, if any.
		/// </summary>
		public object Title
		{
			get { return _title; }
			set
			{
				if (value == _title)
					return;

				_title = value;
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

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}