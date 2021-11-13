using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Metrolib.Sample
{
	public sealed class ToggleButtonsViewModel
		: INotifyPropertyChanged
	{
		private string _selectedItem;

		public ToggleButtonsViewModel()
		{
			SelectedItem = "A";
		}

		public IEnumerable<string> Items => new[] {"A", "B", "C"};

		public string SelectedItem
		{
			get => _selectedItem;
			set
			{
				if (value == _selectedItem)
					return;

				_selectedItem = value;
				EmitPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}