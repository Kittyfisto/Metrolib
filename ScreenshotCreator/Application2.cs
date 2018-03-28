using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Metrolib;
using Metrolib.Controls;

namespace ScreenshotCreator
{
	public sealed class Application2
		: Application
	{
		private static readonly string BasePath;
		private static ResourceDictionary _resourceDictionary;
		private static Dispatcher _dispatcher;
		private static DocumentationCreator _documentationCreator;

		static Application2()
		{
			BasePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
		}

		internal Application2()
		{
			_dispatcher = Dispatcher.CurrentDispatcher;
			_resourceDictionary = new ResourceDictionary
			{
				Source = new Uri("/Metrolib;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
			};
			_documentationCreator = new DocumentationCreator(_dispatcher, _resourceDictionary, typeof(Icons).Assembly);
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
			TakeFilterTextBoxScreenshots();
			TakeFlatPasswordBoxScreenshots();
		}

		private static void TakeFilterTextBoxScreenshots()
		{
			var creator = _documentationCreator.CreateDocumentationFor<FilterTextBox>();

			const int width = 128;
			const int height = 32;

			using (var example = creator.AddExample("Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.Capture();
			}

			using (var example = creator.AddExample("Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.Focus();
				example.Capture();
			}

			using (var example = creator.AddExample("FilterText, Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.Focus();
				example.Capture();
			}

			using (var example = creator.AddExample("FilterText, Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.Capture();
			}

			using (var example = creator.AddExample("Disabled"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.SetValue(UIElement.IsEnabledProperty, false);
				example.Capture();
			}

			creator.SaveAllPoses(BasePath);
		}

		private static void TakeFlatPasswordBoxScreenshots()
		{
			var creator = _documentationCreator.CreateDocumentationFor<FlatPasswordBox>();

			const int width = 128;
			const int height = 32;

			using (var example = creator.AddExample("Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.Capture();
			}

			using (var example = creator.AddExample("Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.Focus();
				example.Capture();
			}

			using (var example = creator.AddExample("Password, Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.Focus();
				example.Capture();
			}

			using (var example = creator.AddExample("Password, Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.Capture();
			}

			using (var example = creator.AddExample("Disabled"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.SetValue(UIElement.IsEnabledProperty, false);
				example.Capture();
			}

			creator.SaveAllPoses(BasePath);
		}
	}
}