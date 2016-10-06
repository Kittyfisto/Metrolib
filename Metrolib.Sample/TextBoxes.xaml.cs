using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Metrolib.Controls;

namespace Metrolib.Sample
{
	public partial class TextBoxes
	{
		private readonly ICommand _dummySearchCommand;

		public static readonly DependencyProperty SearchProperty =
			DependencyProperty.Register("Search", typeof (ISearch), typeof (TextBoxes), new PropertyMetadata(default(ISearch)));

		public ISearch Search
		{
			get { return (ISearch) GetValue(SearchProperty); }
			set { SetValue(SearchProperty, value); }
		}

		public TextBoxes()
		{
			_dummySearchCommand = new DelegateCommand<string>(ExecuteSearch);

			InitializeComponent();
		}

		private void ExecuteSearch(string term)
		{
			Search = new Search();
		}

		public ICommand DummySearchCommand
		{
			get { return _dummySearchCommand; }
		}
	}

	internal sealed class Search
		: ISearch
	{
		private readonly ICommand _previousCommand;
		private readonly ICommand _nextCommand;
		private readonly int _locationCount;
		private int _currentLocationIndex;

		public Search()
		{
			_previousCommand = new DelegateCommand(Previous);
			_nextCommand = new DelegateCommand(Next);
			_locationCount = 5;
		}

		private void Previous()
		{
			var previous = CurrentLocationIndex - 1;
			if (previous < 0)
				previous = _locationCount - 1;
			CurrentLocationIndex = previous;
		}

		private void Next()
		{
			CurrentLocationIndex = (CurrentLocationIndex + 1)%_currentLocationIndex;
		}

		public ICommand PreviousCommand { get { return _previousCommand; } }
		public ICommand NextCommand { get { return _nextCommand; } }
		public int LocationCount { get { return _locationCount; } }
		public int CurrentLocationIndex
		{
			get { return _currentLocationIndex; }
			private set
			{
				if (value == _currentLocationIndex)
					return;

				_currentLocationIndex = value;
				EmitPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}