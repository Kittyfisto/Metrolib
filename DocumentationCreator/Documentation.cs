using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using dotnetdoc;
using Metrolib;
using Metrolib.Controls;

namespace DocumentationCreator
{
	public sealed class Documentation
	{
		private static readonly string BasePath;

		static Documentation()
		{
			BasePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "Documentation");
		}

		public static int Run()
		{
			try
			{
				using (var doc = new Doc(typeof(Icons).Assembly,
				                         "/Metrolib;component/Themes/Generic.xaml"))
				{
					CreateDocumentation(doc);
					doc.RenderTo(BasePath);
				}

				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return -1;
			}
		}

		private static void CreateDocumentation(Doc doc)
		{
			CreateButtonDoc<AddButton>(doc);
			CreateButtonDoc<DeleteButton>(doc);
			CreateButtonDoc<ExportButton>(doc);
			CreateButtonDoc<FolderOpenButton>(doc);
			CreateButtonDoc<MaximizeButton>(doc);
			CreateButtonDoc<MinimizeButton>(doc);
			CreateButtonDoc<MoreButton>(doc);
			CreateButtonDoc<NextButton>(doc);
			CreateButtonDoc<PreviousButton>(doc);
			CreateButtonDoc<RefreshButton>(doc);
			CreateButtonDoc<RemoveButton>(doc);
			CreateButtonDoc<SearchButton>(doc);
			CreateButtonDoc<UndoButton>(doc);

			CreateEditorTextBoxDoc(doc);
			CreateFilterTextBoxDoc(doc);
			CreateSearchTextBoxDoc(doc);
			CreateFlatPasswordBoxDoc(doc);
		}

		private static void CreateButtonDoc<T>(Doc doc) where T : Button, new()
		{
			var creator = doc.CreateDocumentationForFrameworkElement<T>();
			const int width = 32;
			const int height = 32;

			var example1 = creator.AddExample("Unfocused");
			example1.Resize(width, height);

			var example2 = creator.AddExample("Disabled");
			example2.Resize(width, height);
			example2.SetValue(UIElement.IsEnabledProperty, false);
		}

		private static void CreateSearchTextBoxDoc(Doc doc)
		{
			var creator = doc.CreateDocumentationForFrameworkElement<SearchTextBox>();
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

		private static void CreateFilterTextBoxDoc(Doc doc)
		{
			var creator = doc.CreateDocumentationForFrameworkElement<FilterTextBox>();
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

		private static void CreateEditorTextBoxDoc(Doc doc)
		{
			var creator = doc.CreateDocumentationForFrameworkElement<EditorTextBox>();
			const int width = 128;
			const int height = 64;

			var example1 = creator.AddExample("Unfocused");
			example1.Resize(width, height);
			example1.SetValue(EditorTextBox.WatermarkProperty, "Enter comment...");

			var example2 = creator.AddExample("Focused");
			example2.Resize(width, height);
			example2.SetValue(EditorTextBox.WatermarkProperty, "Enter comment...");
			example2.Focus();

			var example3 = creator.AddExample("Text, Focused");
			example3.Resize(width, height);
			example3.SetValue(TextBox.TextProperty, "The quick brown fox jumps over the lazy dog");
			example3.SetValue(EditorTextBox.WatermarkProperty, "Enter comment...");
			example3.SetValue(TextBox.TextWrappingProperty, TextWrapping.Wrap);
			example3.Focus();

			var example4 = creator.AddExample("Text, Unfocused");
			example4.Resize(width, height);
			example4.SetValue(TextBox.TextProperty, "The quick brown fox jumps over the lazy dog");
			example4.SetValue(EditorTextBox.WatermarkProperty, "Enter comment...");
			example4.SetValue(TextBox.TextWrappingProperty, TextWrapping.Wrap);

			var example5 = creator.AddExample("Disabled");
			example5.Resize(width, height);
			example5.SetValue(TextBox.TextProperty, "The quick brown fox jumps over the lazy dog");
			example5.SetValue(EditorTextBox.WatermarkProperty, "Enter comment...");
			example5.SetValue(TextBox.TextWrappingProperty, TextWrapping.Wrap);
			example5.SetValue(UIElement.IsEnabledProperty, false);
		}

		private static void CreateFlatPasswordBoxDoc(Doc doc)
		{
			var creator = doc.CreateDocumentationForFrameworkElement<FlatPasswordBox>();
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