using System;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using log4net;

namespace Netstat
{
	public partial class MainWindow
	{
		private readonly MainWindowViewModel _viewModel;
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly DispatcherTimer _timer;

		public MainWindow(MainWindowViewModel viewModel)
		{
			_viewModel = viewModel;
			_timer = new DispatcherTimer
				{
					Interval = TimeSpan.FromMilliseconds(60)
				};
			_timer.Tick += TimerOnTick;

			Loaded += OnLoaded;
			Unloaded += OnUnloaded;

			InitializeComponent();

			DataContext = viewModel;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_timer.Start();
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_timer.Stop();
		}

		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			try
			{
				_viewModel.Update();
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Caught unexpected exception: {0}", e);
			}
		}
	}
}
