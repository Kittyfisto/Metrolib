using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Metrolib;
using Metrolib.Controls;

namespace DocumentationCreator
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
			CreateFilterTextBoxDoc();
			CreateSearchTextBoxDoc();
			CreateFlatPasswordBoxDoc();
		}

		private static void CreateSearchTextBoxDoc()
		{
			var creator = _documentationCreator.CreateDocumentationFor<SearchTextBox>();

			const int width = 200;
			const int height = 32;

			using (var example = creator.AddExample("Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(SearchTextBox.WatermarkProperty, "Enter search term...");
			}

			using (var example = creator.AddExample("Focused"))
			{
				example.Resize(width, height);
				example.SetValue(SearchTextBox.WatermarkProperty, "Enter search term...");
				example.Focus();
			}
			
			using (var example = creator.AddExample("FilterText, Focused"))
			{
				example.Resize(width, height);
				example.SetValue(TextBox.TextProperty, "Luke");
				example.SetValue(SearchTextBox.WatermarkProperty, "Enter search term...");
				example.Focus();
			}

			creator.SaveAllPoses(BasePath);
		}

		private static void CreateFilterTextBoxDoc()
		{
			var creator = _documentationCreator.CreateDocumentationFor<FilterTextBox>();

			const int width = 128;
			const int height = 32;

			using (var example = creator.AddExample("Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
			}

			using (var example = creator.AddExample("Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.Focus();
			}

			using (var example = creator.AddExample("FilterText, Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.Focus();
			}

			using (var example = creator.AddExample("FilterText, Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
			}

			using (var example = creator.AddExample("Invalid FilterText"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.SetValue(FilterTextBox.IsValidProperty, false);
			}

			using (var example = creator.AddExample("Disabled"))
			{
				example.Resize(width, height);
				example.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
				example.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
				example.SetValue(UIElement.IsEnabledProperty, false);
			}

			creator.SaveAllPoses(BasePath);
		}

		private static void CreateFlatPasswordBoxDoc()
		{
			var creator = _documentationCreator.CreateDocumentationFor<FlatPasswordBox>();

			const int width = 128;
			const int height = 32;

			using (var example = creator.AddExample("Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
			}

			using (var example = creator.AddExample("Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.Focus();
			}

			using (var example = creator.AddExample("Password, Focused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.Focus();
			}

			using (var example = creator.AddExample("Password, Unfocused"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
			}

			using (var example = creator.AddExample("Disabled"))
			{
				example.Resize(width, height);
				example.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
				example.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
				example.SetValue(UIElement.IsEnabledProperty, false);
			}

			creator.SaveAllPoses(BasePath);
		}
	}
}