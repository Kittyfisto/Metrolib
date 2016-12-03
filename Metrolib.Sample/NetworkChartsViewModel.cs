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
		private IEnumerable<AvengerViewModel> _avengers;
		private List<Edge<AvengerViewModel>> _dislikes;

		public NetworkChartsViewModel()
		{
			var captainAmerica = new AvengerViewModel
				{
					Name = "Captain America",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/CaptainAmerica.png")
				};
			var ironMan = new AvengerViewModel
				{
					Name = "Iron Man",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/IronMan.png")
				};
			var hulk = new AvengerViewModel
				{
					Name = "Hulk",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/Hulk.png")
				};
			var thor = new AvengerViewModel
				{
					Name = "Thor",
					Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/Thor.png")
				};
			_avengers = new List<AvengerViewModel>
				{
					captainAmerica,
					ironMan,
					hulk,
					thor
				};
			_dislikes = new List<Edge<AvengerViewModel>>
				{
					Edge.Create(captainAmerica, ironMan),
					Edge.Create(ironMan, hulk),
					Edge.Create(hulk, captainAmerica),
					Edge.Create(hulk, thor),
				};

			_resetCommand = new DelegateCommand(Reset);
		}

		public ICommand ResetCommand
		{
			get { return _resetCommand; }
		}

		public List<Edge<AvengerViewModel>> Dislikes
		{
			get { return _dislikes; }
			set
			{
				_dislikes = value;
				EmitPropertyChanged();
			}
		}

		public IEnumerable<AvengerViewModel> Avengers
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
			List<Edge<AvengerViewModel>> d = Dislikes;
			IEnumerable<AvengerViewModel> a = Avengers;

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