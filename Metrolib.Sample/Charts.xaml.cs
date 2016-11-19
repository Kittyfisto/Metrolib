using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Metrolib.Sample
{
	public partial class Charts
	{
		private readonly ObservableCollection<Point> _curve;
		private readonly DispatcherTimer _timer;
		private readonly Random _random;

		public Charts()
		{
			InitializeComponent();

			_curve = new ObservableCollection<Point>(Enumerable.Range(0, 8).Select(x => new Point()));

			PART_Chart.Series = new[]
				{
					new LineSeries
						{
							Fill = Brushes.LightSkyBlue,
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
						},
					new LineSeries
						{
							Values = _curve,
							Fill = null,
							Outline = new Pen(Brushes.OrangeRed, 2)
						}
				};

			_random = new Random();
			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromMilliseconds(60);
			_timer.Tick += TimerOnTick;
			_timer.Start();
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			for (int i = 0; i < _curve.Count; ++i)
			{
				_curve[i] = new Point(i, _random.NextDouble());
			}
		}
	}
}