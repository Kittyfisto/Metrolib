using System.Windows;
using System.Windows.Controls;
using System.Windows.Shell;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Base class for a window without borders.
	///     Styled to look like a window in Windows 10.
	/// </summary>
	[TemplatePart(Name = "PART_MinimizeWindow", Type = typeof (Button))]
	[TemplatePart(Name = "PART_MaximizeWindow", Type = typeof (MaximizeButton))]
	[TemplatePart(Name = "PART_CloseWindow", Type = typeof (Button))]
	public class ChromelessWindow
		: Window
	{
		/// <summary>
		///     Definition of the <see cref="VerticalHeaderAlignment" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty VerticalHeaderAlignmentProperty =
			DependencyProperty.Register("VerticalHeaderAlignment", typeof (VerticalAlignment), typeof (ChromelessWindow),
			                            new PropertyMetadata(default(VerticalAlignment)));

		/// <summary>
		///     Definition of the <see cref="HorizontalHeaderAlignment" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HorizontalHeaderAlignmentProperty =
			DependencyProperty.Register("HorizontalHeaderAlignment", typeof (HorizontalAlignment), typeof (
				                                                                                       ChromelessWindow),
			                            new PropertyMetadata(default(HorizontalAlignment)));

		/// <summary>
		///     Definition of the <see cref="Header" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HeaderProperty =
			DependencyProperty.Register("Header", typeof (object), typeof (ChromelessWindow),
			                            new PropertyMetadata(default(object)));

		/// <summary>
		///     Definition of the <see cref="HeaderTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HeaderTemplateProperty =
			DependencyProperty.Register("HeaderTemplate", typeof (DataTemplate), typeof (ChromelessWindow),
			                            new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		///     Definition of the <see cref="HeaderTemplateSelector" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HeaderTemplateSelectorProperty =
			DependencyProperty.Register("HeaderTemplateSelector", typeof (DataTemplateSelector),
			                            typeof (ChromelessWindow), new PropertyMetadata(default(DataTemplateSelector)));

		/// <summary>
		///     Definition of the <see cref="TitleBarHeight" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty TitleBarHeightProperty =
			DependencyProperty.Register("TitleBarHeight", typeof (double), typeof (ChromelessWindow),
			                            new PropertyMetadata(default(double), OnTitleBarHeightChanged));

		private Button _closeWindow;
		private MaximizeButton _maximizeWindow;
		private Button _minimizeWindow;

		static ChromelessWindow()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ChromelessWindow),
			                                         new FrameworkPropertyMetadata(typeof (ChromelessWindow)));
		}

		/// <summary>
		///     The height of the title bar of this window.
		///     Set to 32 by default.
		/// </summary>
		public double TitleBarHeight
		{
			get { return (double) GetValue(TitleBarHeightProperty); }
			set { SetValue(TitleBarHeightProperty, value); }
		}

		/// <summary>
		///     The vertical alignment of the content inside the title bar.
		/// </summary>
		public VerticalAlignment VerticalHeaderAlignment
		{
			get { return (VerticalAlignment) GetValue(VerticalHeaderAlignmentProperty); }
			set { SetValue(VerticalHeaderAlignmentProperty, value); }
		}

		/// <summary>
		///     The horizontal alignment of the content inside the title bar.
		/// </summary>
		public HorizontalAlignment HorizontalHeaderAlignment
		{
			get { return (HorizontalAlignment) GetValue(HorizontalHeaderAlignmentProperty); }
			set { SetValue(HorizontalHeaderAlignmentProperty, value); }
		}

		/// <summary>
		///     The template selector for the content inside the title bar.
		/// </summary>
		public DataTemplateSelector HeaderTemplateSelector
		{
			get { return (DataTemplateSelector) GetValue(HeaderTemplateSelectorProperty); }
			set { SetValue(HeaderTemplateSelectorProperty, value); }
		}

		/// <summary>
		///     The template for the content inside the title bar.
		/// </summary>
		public DataTemplate HeaderTemplate
		{
			get { return (DataTemplate) GetValue(HeaderTemplateProperty); }
			set { SetValue(HeaderTemplateProperty, value); }
		}

		/// <summary>
		///     Additional content inside the title bar.
		/// </summary>
		public object Header
		{
			get { return GetValue(HeaderProperty); }
			set { SetValue(HeaderProperty, value); }
		}

		private static void OnTitleBarHeightChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((ChromelessWindow) dependencyObject).OnTitleBarHeightChanged((double) args.NewValue);
		}

		private void OnTitleBarHeightChanged(double height)
		{
			WindowChrome chrome = WindowChrome.GetWindowChrome(this);
			if (chrome != null)
				chrome.CaptionHeight = height;
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (_minimizeWindow != null)
				_minimizeWindow.Click -= MinimizeWindowOnClick;
			_minimizeWindow = (Button) GetTemplateChild("PART_MinimizeWindow");
			if (_minimizeWindow != null)
				_minimizeWindow.Click += MinimizeWindowOnClick;

			if (_maximizeWindow != null)
				_maximizeWindow.Click -= MaximizeWindowOnClick;
			_maximizeWindow = (MaximizeButton) GetTemplateChild("PART_MaximizeWindow");
			if (_maximizeWindow != null)
			{
				_maximizeWindow.Click += MaximizeWindowOnClick;
				_maximizeWindow.IsMaximized = WindowState == WindowState.Maximized;
			}

			if (_closeWindow != null)
				_closeWindow.Click -= CloseWindowOnClick;
			_closeWindow = (Button) GetTemplateChild("PART_CloseWindow");
			if (_closeWindow != null)
				_closeWindow.Click += CloseWindowOnClick;

			OnTitleBarHeightChanged(TitleBarHeight);
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