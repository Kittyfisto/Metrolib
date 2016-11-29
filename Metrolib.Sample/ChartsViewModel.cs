using System;
using System.Collections.Generic;

namespace Metrolib.Sample
{
	public sealed class ChartsViewModel
	{
		private readonly List<AvengerViewModel> _avengers;

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
					new AvengerViewModel
						{
							Name = "Hulk",
							Portrait = new Uri("pack://application:,,,/Metrolib.Sample;component/Resources/Hulk.png")
						}
				};
		}

		public IEnumerable<AvengerViewModel> Avengers
		{
			get { return _avengers; }
		}
	}
}