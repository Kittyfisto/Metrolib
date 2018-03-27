using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Metrolib.Controls;

namespace ScreenshotCreator
{
	public sealed class Application2
		: Application
	{
		private static readonly string BasePath;
		private static ResourceDictionary _resourceDictionary;
		private static Dispatcher _dispatcher;

		static Application2()
		{
			BasePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Samples");
		}

		internal Application2()
		{
			_dispatcher = Dispatcher.CurrentDispatcher;
			_resourceDictionary = new ResourceDictionary
			{
				Source = new Uri("/Metrolib;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
			};
		}

		public new static int Run()
		{
			try
			{
				StartApplication();

				TakeScreenshots();

				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return -1;
			}
			finally
			{
				_dispatcher.BeginInvokeShutdown(DispatcherPriority.Background);
			}
		}

		private static void StartApplication()
		{
			var manualResetEvent = new ManualResetEvent(false);
			var thread = new Thread(() =>
			{
				var application = new Application2();
				manualResetEvent.Set();
				((Application) application).Run();
			})
			{
				IsBackground = true
			};
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();

			manualResetEvent.WaitOne();
		}

		private static void TakeScreenshots()
		{
			TakeFlatPasswordBoxScreenshots();
		}

		private static void TakeFlatPasswordBoxScreenshots()
		{
			var creator = PrepareSnapshotsFor<FlatPasswordBox>();

			const int width = 128;
			const int height = 32;

			using (var pose = creator.AddPose("Default"))
			{
				pose.Resize(width, height);
				pose.Prepare(box => box.Watermark = "Enter password...");
				pose.Capture();
			}

			using (var pose = creator.AddPose("Focused"))
			{
				pose.Resize(width, height);
				pose.Prepare(box => box.Watermark = "Enter password...");
				pose.Focus();
				pose.Capture();
			}

			using (var pose = creator.AddPose("PasswordFocused"))
			{
				pose.Resize(width, height);
				pose.Prepare(box => box.Password = "Secret");
				pose.Focus();
				pose.Capture();
			}

			using (var pose = creator.AddPose("PasswordUnfocused"))
			{
				pose.Resize(width, height);
				pose.Prepare(box => box.Password = "Secret");
				pose.Capture();
			}

			using (var pose = creator.AddPose("Disabled"))
			{
				pose.Resize(width, height);
				pose.Prepare(box => box.Password = "Secret");
				pose.Disable();
				pose.Capture();
			}

			creator.SaveAllSnapshots(BasePath);
		}

		private static SnapshotCreator<T> PrepareSnapshotsFor<T>() where T : FrameworkElement, new()
		{
			var creator = new SnapshotCreator<T>(_dispatcher, _resourceDictionary);
			return creator;
		}
	}
}