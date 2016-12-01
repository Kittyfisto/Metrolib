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

		public LineCharts()
		{
			InitializeComponent();

			_curve = new ObservableCollection<Point>(Enumerable.Range(0, 101).Select(x => new Point()));

			PART_Chart.XAxisCaption = "Frequency";
			PART_Chart.YAxisCaption = "dBm";
			PART_Chart.Series = new[]
				{
					new LineSeries
						{
							PointRadius = 5,
							PointFill = Brushes.DeepSkyBlue,

							Fill = Brushes.LightSkyBlue,
							/*Values = new List<Point>
								{
									new Point(0, 0),
									new Point(1, 1),
									new Point(2, 0),
								}*/
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
									new Point(8, 1),
									new Point(9, 1.3),
									new Point(10, 1.3),
								}
						},
					new LineSeries
						{
							Fill = Brushes.LightSalmon,
							Outline = new Pen(Brushes.OrangeRed, 2),
							
							PointRadius = 5,
							PointFill = Brushes.LightSalmon,
							PointOutline = new Pen(Brushes.OrangeRed, 2),
							
							//Values = _curve,
							Values = new List<Point>
								{
									new Point(0, 1),
									new Point(1, 1),
									new Point(2, 1),
									new Point(3, 1),
									new Point(4, 1),
									new Point(5, 1),
									new Point(6, 1),
									new Point(7, 1),
									new Point(8, 1),
									new Point(9, 1),
									new Point(10, 1),
								}
						}
				};

			_random = new Random();
			_timer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMilliseconds(60)
				};
			_timer.Tick += TimerOnTick;
			_timer.Start();

			Normal.IsChecked = true;
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