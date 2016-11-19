using System;
using System.Collections.Generic;
using System.Windows;

namespace Metrolib.Sample
{
	public partial class Charts
	{
		public Charts()
		{
			InitializeComponent();

			PART_Chart.Series = new[]
				{
					new LineSeries
						{
							Values = new List<Point>
								{
									new Point(0, 0),
									new Point(1, 0.5),
									new Point(2, 1),
									new Point(3, 0.75),
									new Point(4, 2),
									new Point(5, -1),
									new Point(6, 0.25),
									new Point(7, 0.3),
								}
						}
				};
		}
	}
}