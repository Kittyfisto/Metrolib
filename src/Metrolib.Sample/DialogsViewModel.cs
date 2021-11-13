using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Metrolib.Sample
{
	public sealed class DialogsViewModel
		: INotifyPropertyChanged
	{
		public ICommand ShowFlatBannerCommand => new DelegateCommand2(ShowFlatBanner);

		private Visibility _flatBannerVisibility;

		public DialogsViewModel()
		{
			FlatBannerVisibility = Visibility.Collapsed;
		}

		public Visibility FlatBannerVisibility
		{
			get => _flatBannerVisibility;
			set
			{
				if (value == _flatBannerVisibility)
					return;

				_flatBannerVisibility = value;
				EmitPropertyChanged();
			}
		}

		private void ShowFlatBanner()
		{
			FlatBannerVisibility = Visibility.Visible;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}