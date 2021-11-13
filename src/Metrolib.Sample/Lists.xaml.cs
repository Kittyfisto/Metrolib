using System.Collections.Generic;

namespace Metrolib.Sample
{
	public partial class Lists
	{
		public Lists()
		{
			InitializeComponent();
		}

		public List<string> Items
		{
			get
			{
				return new List<string>
					{
						"Luke Cage",
						"Daredevil",
						"Jessica Jones",
						"Punisher",
						"Steve Rogers",
						"Bruce Banner",
						"Tony Stark",
						"Thor",
						"Natasha Romanoff",
						"Clint Barton",
						"Loki",
						"Phil Coulson",
						"Maria Hill",
						"Nick Fury"
					};
			}
		}
	}
}