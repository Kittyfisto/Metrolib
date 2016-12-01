using System;
using System.Collections.Generic;

namespace Metrolib.Sample
{
	public sealed class ChartsViewModel
	{
		private readonly List<AvengerViewModel> _avengers;
		private readonly List<Edge<AvengerViewModel>> _dislikes;

		public ChartsViewModel()
		{
			_avengers = new List<AvengerViewModel>
				{
					new AvengerViewModel
						{
							Name = "Captain America",
							Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/CaptainAmerica.png")
						},
					new AvengerViewModel
						{
							Name = "Iron Man",
							Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/IronMan.png")
						},
					/*new AvengerViewModel
						{
							Name = "Hulk",
							Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/Hulk.png")
						}*/
				};
			_dislikes = new List<Edge<AvengerViewModel>>
				{
					Edge.Create(_avengers[0], _avengers[1]),
					/*Edge.Create(_avengers[1], _avengers[2]),
					Edge.Create(_avengers[2], _avengers[0]),*/
				};
		}

		public List<Edge<AvengerViewModel>> Dislikes
		{
			get { return _dislikes; }
		}

		public IEnumerable<AvengerViewModel> Avengers
		{
			get { return _avengers; }
		}
	}
}