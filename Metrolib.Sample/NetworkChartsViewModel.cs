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
		private readonly List<EdgeType> _edgeTypes;
		private readonly ICommand _resetCommand;
		private IEnumerable<MarvelCharacterViewModel> _avengers;
		private EdgeType _selectedEdgeType;
		private IEnumerable<IEdge> _selectedEdges;

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
			var falcon = new MarvelCharacterViewModel
				{
					Name = "Falcon",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/Falcon.png")
				};
			var sharonCarter = new MarvelCharacterViewModel
				{
					Name = "Sharon Carter",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/SharonCarter.png")
				};
			var vision = new MarvelCharacterViewModel
				{
					Name = "Vision",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/vision.png")
				};
			var mariaHill = new MarvelCharacterViewModel
				{
					Name = "Maria Hill",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/MariaHill.png")
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
					scarletWitch,
					falcon,
					sharonCarter,
					vision,
					mariaHill
				};

			var edges1 = new List<IEdge>
				{
					Edge.Create(captainAmerica, ironMan),
					Edge.Create(ironMan, hulk),
					Edge.Create(hulk, captainAmerica),
					Edge.Create(hulk, thor),
					Edge.Create(hulk, blackWidow),
					Edge.Create(thor, blackWidow),
					Edge.Create(blackWidow, ironMan),
					Edge.Create(captainAmerica, winterSoldier),
					Edge.Create(captainAmerica, nickFury),
					Edge.Create(nickFury, blackWidow),
					Edge.Create(captainAmerica, scarletWitch),
					Edge.Create(captainAmerica, falcon),
					Edge.Create(falcon, blackWidow),
					Edge.Create(sharonCarter, nickFury),
					Edge.Create(sharonCarter, captainAmerica),
					Edge.Create(vision, scarletWitch),
					Edge.Create(vision, captainAmerica),
					Edge.Create(mariaHill, captainAmerica),
				};

			var edges2 = new List<IEdge>
				{
					Edge.Create(captainAmerica, hulk),
					Edge.Create(hulk, mariaHill),
					Edge.Create(mariaHill, nickFury),
					Edge.Create(scarletWitch, blackWidow),
					Edge.Create(falcon, nickFury),
					Edge.Create(vision, sharonCarter),
					Edge.Create(thor, blackWidow),
					Edge.Create(winterSoldier, captainAmerica),
					Edge.Create(falcon, thor),
					Edge.Create(ironMan, winterSoldier),
					Edge.Create(vision, thor),
					Edge.Create(sharonCarter, ironMan),
					Edge.Create(scarletWitch, winterSoldier),
				};

			var edges3 = CreateCircleAround(captainAmerica, _avengers);

			var edges4 = new List<IEdge>
				{
					Edge.Create(captainAmerica, hulk),
					Edge.Create(hulk, nickFury),
					Edge.Create(nickFury, scarletWitch),
					Edge.Create(scarletWitch, captainAmerica),
					Edge.Create(nickFury, captainAmerica),
					Edge.Create(hulk, scarletWitch),
					
					Edge.Create(hulk, blackWidow),
					Edge.Create(blackWidow, sharonCarter),
					Edge.Create(sharonCarter, captainAmerica),
					Edge.Create(hulk, sharonCarter),
					Edge.Create(blackWidow, captainAmerica),
					
					Edge.Create(blackWidow, vision),
					Edge.Create(vision, mariaHill),
					Edge.Create(mariaHill, sharonCarter),
					Edge.Create(blackWidow, mariaHill),
					Edge.Create(vision, sharonCarter),
					
					Edge.Create(vision, winterSoldier),
					Edge.Create(winterSoldier, falcon),
					Edge.Create(falcon, mariaHill),
					Edge.Create(mariaHill, winterSoldier),
					Edge.Create(vision, falcon),
					
					Edge.Create(winterSoldier, thor),
					Edge.Create(thor, ironMan),
					Edge.Create(ironMan, falcon),
					Edge.Create(winterSoldier, ironMan),
					Edge.Create(falcon, thor),
				};

			_edgeTypes = new List<EdgeType>
				{
					new EdgeType("Variant \"Coordination\" A", edges1),
					new EdgeType("Variant \"Whatever\" B", edges2),
					new EdgeType("Variant \"Assemble\" C", edges3),
					new EdgeType("Variant \"Chain\" D", edges4)
				};
			SelectedEdgeType = _edgeTypes[0];

			_resetCommand = new DelegateCommand(Reset);
		}

		private IEnumerable<IEdge> CreateCircleAround(MarvelCharacterViewModel center, IEnumerable<MarvelCharacterViewModel> avengers)
		{
			var ret = new List<IEdge>();
			foreach (var avenger in avengers)
			{
				if (!Equals(center, avenger))
					ret.Add(Edge.Create(center, avenger));
			}
			return ret;
		}

		public List<EdgeType> EdgeTypes
		{
			get { return _edgeTypes; }
		}

		public IEnumerable<IEdge> SelectedEdges
		{
			get { return _selectedEdges; }
			private set
			{
				_selectedEdges = value;
				EmitPropertyChanged();
			}
		}

		public EdgeType SelectedEdgeType
		{
			get { return _selectedEdgeType; }
			set
			{
				if (value == _selectedEdgeType)
					return;

				_selectedEdgeType = value;
				EmitPropertyChanged();

				SelectedEdges = value != null ? value.Edges : null;
			}
		}

		public ICommand ResetCommand
		{
			get { return _resetCommand; }
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
			var avengers = Avengers;
			var edges = SelectedEdges;

			Avengers = null;
			SelectedEdges = null;

			Avengers = avengers;
			SelectedEdges = edges;
		}

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}