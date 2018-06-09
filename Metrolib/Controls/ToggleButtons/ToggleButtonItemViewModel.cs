using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Metrolib.Controls.ToggleButtons
{
	/// <summary>
	/// </summary>
	public sealed class ToggleButtonItemViewModel
		: INotifyPropertyChanged
	{
		private readonly object _item;
		private bool _isSelected;

		/// <summary>
		/// </summary>
		/// <param name="item"></param>
		public ToggleButtonItemViewModel(object item)
		{
			_item = item;
		}

		/// <summary>
		/// </summary>
		public object Item => _item;

		/// <summary>
		/// </summary>
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				if (value == _isSelected)
					return;

				_isSelected = value;
				EmitPropertyChanged();
			}
		}

		/// <inheritdoc />
		public event PropertyChangedEventHandler PropertyChanged;

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}