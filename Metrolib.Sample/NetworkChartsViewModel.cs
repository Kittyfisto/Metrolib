using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Metrolib.Sample
{
	public sealed class NetworkChartsViewModel
		: INotifyPropertyChanged
	{
		private readonly ICommand _resetCommand;
		private IEnumerable<MarvelCharacterViewModel> _avengers;
		private List<Edge<MarvelCharacterViewModel>> _dislikes;

		public NetworkChartsViewModel()
		{
			var captainAmerica = new MarvelCharacterViewModel
				{
					Name = "Captain America",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/CaptainAmerica.png")
				};
			var ironMan = new MarvelCharacterViewModel
				{
					Name = "Iron Man",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/IronMan.png")
				};
			var hulk = new MarvelCharacterViewModel
				{
					Name = "Hulk",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/Hulk.png")
				};
			var thor = new MarvelCharacterViewModel
				{
					Name = "Thor",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/Thor.png")
				};
			var blackWidow = new MarvelCharacterViewModel
				{
					Name = "Black Widow",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/BlackWidow.png")
				};
			var winterSoldier = new MarvelCharacterViewModel
			{
				Name = "Winter Soldier",
				Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/WinterSoldier.png")
			};
			var nickFury = new MarvelCharacterViewModel
				{
					Name = "Nick Fury",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/NickFury.png")
				};
			var scarletWitch = new MarvelCharacterViewModel
			{
				Name = "Scarlet Witch",
				Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/ScarletWitch.png")
			};
			_avengers = new List<MarvelCharacterViewModel>
				{
					captainAmerica,
					ironMan,
					hulk,
					thor,
					blackWidow,
					winterSoldier,
					nickFury,
					scarletWitch
				};
			_dislikes = new List<Edge<MarvelCharacterViewModel>>
				{
					Edge.Create(captainAmerica, ironMan),
					Edge.Create(ironMan, hulk),
					Edge.Create(hulk, captainAmerica),
					Edge.Create(hulk, thor),
					Edge.Create(hulk, blackWidow),
					Edge.Create(thor, blackWidow),
					Edge.Create(blackWidow, ironMan),
					Edge.Create(ironMan, winterSoldier),
					Edge.Create(captainAmerica, nickFury),
					Edge.Create(nickFury, blackWidow),
					Edge.Create(captainAmerica, scarletWitch),
				};

			_resetCommand = new DelegateCommand(Reset);
		}

		public ICommand ResetCommand
		{
			get { return _resetCommand; }
		}

		public List<Edge<MarvelCharacterViewModel>> Dislikes
		{
			get { return _dislikes; }
			set
			{
				_dislikes = value;
				EmitPropertyChanged();
			}
		}

		public IEnumerable<MarvelCharacterViewModel> Avengers
		{
			get { return _avengers; }
			set
			{
				_avengers = value;
				EmitPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void Reset()
		{
			List<Edge<MarvelCharacterViewModel>> d = Dislikes;
			IEnumerable<MarvelCharacterViewModel> a = Avengers;

			Dislikes = null;
			Avengers = null;

			Avengers = a;
			Dislikes = d;
		}

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}