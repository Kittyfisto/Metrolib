using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public sealed class PieSeries
		: IPieSeries
		  , INotifyPropertyChanged
	{
		private List<IPieSlice> _slices;

		/// <summary>
		/// Initializes this series.
		/// </summary>
		public PieSeries()
		{
			_slices = new List<IPieSlice>();
		}

		/// <summary>
		/// Occurs when a property value changes.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		IEnumerable<IPieSlice> IPieSeries.Slices
		{
			get { return _slices; }
		}

		/// <summary>
		/// 
		/// </summary>
		public List<IPieSlice> Slices
		{
			get { return _slices; }
			set
			{
				if (value == _slices)
					return;

				_slices = value;
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