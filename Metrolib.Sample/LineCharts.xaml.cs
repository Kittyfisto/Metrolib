using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Metrolib.Sample
{
	public partial class LineCharts
	{
		private readonly ObservableCollection<Point> _curve;
		private readonly DispatcherTimer _timer;
		private readonly Random _random;
		private readonly List<Point> _values;

		public LineCharts()
		{
			InitializeComponent();

			_curve = new ObservableCollection<Point>(Enumerable.Range(0, 101).Select(x => new Point()));

			_random = new Random();
			_timer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMilliseconds(60)
				};
			_timer.Tick += TimerOnTick;
			_timer.Start();

			Stacked.IsChecked = true;
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			/*for (int i = 0; i < _curve.Count; ++i)
			{
				double y = Math.Cos(i/10.0) + _random.NextDouble() / 4;
				_curve[i] = new Point(1.0*i/10, y);
			}*/
		}

		private void OnNormalChecked(object sender, RoutedEventArgs e)
		{
			PART_Chart.ChartType = LineChartType.Normal;
		}

		private void OnStackedChecked(object sender, RoutedEventArgs e)
		{
			PART_Chart.ChartType = LineChartType.Stacked;
		}
	}
}