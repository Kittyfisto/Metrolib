using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Metrolib.Controls.Charts.Line.Canvas
{
	/// <summary>
	///     Describes the x- or y-axis of a <see cref="LineChart" />.
	/// </summary>
	public sealed class Axis
		: INotifyPropertyChanged
	{
		private object _caption;
		private bool _showLines;
		private bool _showTicks;

		/// <summary>
		/// 
		/// </summary>
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
		/// 
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
		/// 
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
	}
}