using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using dotnetdoc;
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
		private static AssemblyDocumentationCreator _docCreator;

		static Application2()
		{
			BasePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Documentation");
		}

		internal Application2()
		{
			_dispatcher = Dispatcher.CurrentDispatcher;
			_resourceDictionary = new ResourceDictionary
			{
				Source = new Uri("/Metrolib;component/Themes/Generic.xaml", UriKind.RelativeOrAbsolute)
			};
			_docCreator = new AssemblyDocumentationCreator(typeof(Icons).Assembly,
			                                               _dispatcher,
			                                               _resourceDictionary);
		}

		public new static int Run()
		{
			try
			{
				StartApplication();

				CreateDocumentation();

				_docCreator.RenderTo(BasePath);

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

		private static void CreateDocumentation()
		{
			CreateFilterTextBoxDoc();
			CreateSearchTextBoxDoc();
			CreateFlatPasswordBoxDoc();
		}

		private static void CreateSearchTextBoxDoc()
		{
			var creator = _docCreator.CreateDocumentationForFrameworkElement<SearchTextBox>();
			const int width = 200;
			const int height = 32;

			var example1 = creator.AddExample("Unfocused");
			example1.Resize(width, height);
			example1.SetValue(SearchTextBox.WatermarkProperty, "Enter search term...");

			var example2 = creator.AddExample("Focused");
			example2.Resize(width, height);
			example2.SetValue(SearchTextBox.WatermarkProperty, "Enter search term...");
			example2.Focus();

			var example3 = creator.AddExample("FilterText, Focused");
			example3.Resize(width, height);
			example3.SetValue(TextBox.TextProperty, "Luke");
			example3.SetValue(SearchTextBox.WatermarkProperty, "Enter search term...");
			example3.Focus();
		}

		private static void CreateFilterTextBoxDoc()
		{
			var creator = _docCreator.CreateDocumentationForFrameworkElement<FilterTextBox>();
			const int width = 128;
			const int height = 32;

			var example1 = creator.AddExample("Unfocused");
			example1.Resize(width, height);
			example1.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");

			var example2 = creator.AddExample("Focused");
			example2.Resize(width, height);
			example2.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
			example2.Focus();

			var example3 = creator.AddExample("FilterText, Focused");
			example3.Resize(width, height);
			example3.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
			example3.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
			example3.Focus();

			var example4 = creator.AddExample("FilterText, Unfocused");
			example4.Resize(width, height);
			example4.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
			example4.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");

			var example5 = creator.AddExample("Invalid FilterText");
			example5.Resize(width, height);
			example5.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
			example5.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
			example5.SetValue(FilterTextBox.IsValidProperty, false);

			var example6 = creator.AddExample("Disabled");
			example6.Resize(width, height);
			example6.SetValue(FilterTextBox.FilterTextProperty, "[0-9]+");
			example6.SetValue(FilterTextBox.WatermarkProperty, "Enter filter...");
			example6.SetValue(UIElement.IsEnabledProperty, false);
		}

		private static void CreateFlatPasswordBoxDoc()
		{
			var creator = _docCreator.CreateDocumentationForFrameworkElement<FlatPasswordBox>();
			const int width = 128;
			const int height = 32;

			var example1 = creator.AddExample("Unfocused");
			example1.Resize(width, height);
			example1.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");

			var example2 = creator.AddExample("Focused");
			example2.Resize(width, height);
			example2.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
			example2.Focus();

			var example3 = creator.AddExample("Password, Focused");
			example3.Resize(width, height);
			example3.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
			example3.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
			example3.Focus();

			var example4 = creator.AddExample("Password, Unfocused");
			example4.Resize(width, height);
			example4.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
			example4.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");

			var example5 = creator.AddExample("Disabled");
			example5.Resize(width, height);
			example5.SetValue(FlatPasswordBox.PasswordProperty, "Secret");
			example5.SetValue(FlatPasswordBox.WatermarkProperty, "Enter password...");
			example5.SetValue(UIElement.IsEnabledProperty, false);
		}
	}
}