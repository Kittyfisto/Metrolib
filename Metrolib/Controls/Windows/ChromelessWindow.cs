using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Base class for a window without borders.
	///     Styled to look like a window in Windows 10.
	/// </summary>
	[TemplatePart(Name = "PART_MinimizeWindow", Type = typeof(Button))]
	[TemplatePart(Name = "PART_MaximizeWindow", Type = typeof(MaximizeButton))]
	[TemplatePart(Name = "PART_CloseWindow", Type = typeof(Button))]
	public class ChromelessWindow
		: Window
	{
		private Button _minimizeWindow;
		private MaximizeButton _maximizeWindow;
		private Button _closeWindow;

		static ChromelessWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ChromelessWindow),
			                                         new FrameworkPropertyMetadata(typeof (ChromelessWindow)));
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (_minimizeWindow != null)
				_minimizeWindow.Click -= MinimizeWindowOnClick;
			_minimizeWindow = (Button)GetTemplateChild("PART_MinimizeWindow");
			if (_minimizeWindow != null)
				_minimizeWindow.Click += MinimizeWindowOnClick;

			if (_maximizeWindow != null)
				_maximizeWindow.Click -= MaximizeWindowOnClick;
			_maximizeWindow = (MaximizeButton)GetTemplateChild("PART_MaximizeWindow");
			if (_maximizeWindow != null)
			{
				_maximizeWindow.Click += MaximizeWindowOnClick;
				_maximizeWindow.IsMaximized = WindowState == WindowState.Maximized;
			}

			if (_closeWindow != null)
				_closeWindow.Click -= CloseWindowOnClick;
			_closeWindow = (Button)GetTemplateChild("PART_CloseWindow");
			if (_closeWindow != null)
				_closeWindow.Click += CloseWindowOnClick;
		}

		private void MinimizeWindowOnClick(object sender, RoutedEventArgs e)
		{
			WindowState = WindowState.Minimized;
		}

		private void MaximizeWindowOnClick(object sender, RoutedEventArgs e)
		{
			if (WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
				_maximizeWindow.IsMaximized = false;
			}
			else
			{
				WindowState = WindowState.Maximized;
				_maximizeWindow.IsMaximized = true;
			}
		}

		private void CloseWindowOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			Close();
		}
	}
}