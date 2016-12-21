using System;
using System.Windows;
using System.Windows.Threading;

namespace Metrolib.Sample
{
	public partial class LineCharts
	{
		private readonly DispatcherTimer _timer;

		public LineCharts()
		{
			InitializeComponent();

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