using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using log4net;

namespace Metrolib.Controls
{
	/// <summary>
	///     A control to display a hyperlink to a user supplied URI.
	///     Opens the default browser when clicked.
	/// </summary>
	public class OpenInNewHyperlink
		: Control
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static readonly DependencyProperty NavigateUriProperty =
			DependencyProperty.Register("NavigateUri", typeof (Uri), typeof (OpenInNewHyperlink),
			                            new PropertyMetadata(default(Uri)));

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof (string), typeof (OpenInNewHyperlink),
			                            new PropertyMetadata(default(string)));

		private static readonly DependencyPropertyKey IsPressedPropertyKey
			= DependencyProperty.RegisterReadOnly("IsPressed", typeof (bool), typeof (OpenInNewHyperlink),
			                                      new FrameworkPropertyMetadata(default(bool),
			                                                                    FrameworkPropertyMetadataOptions.None));

		public static readonly DependencyProperty IsPressedProperty
			= IsPressedPropertyKey.DependencyProperty;

		static OpenInNewHyperlink()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (OpenInNewHyperlink),
			                                         new FrameworkPropertyMetadata(typeof (OpenInNewHyperlink)));
		}

		public OpenInNewHyperlink()
		{
			MouseLeftButtonDown += OnMouseLeftButtonDown;
			MouseLeftButtonUp += OnMouseLeftButtonUp;
		}

		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			IsPressed = false;

			if (IsMouseCaptured)
			{
				ReleaseMouseCapture();
				OpenBrowser();
				e.Handled = true;
			}
		}

		private void OpenBrowser()
		{
			if (NavigateUri == null)
			{
				Log.DebugFormat("No NavigateUri set, can't open browser!");
				return;
			}

			var uri = NavigateUri.ToString();

			try
			{
				Process.Start(new ProcessStartInfo(uri));
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Cauhgt unexpected exception: {0}", e);
			}
		}

		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			IsPressed = true;
			CaptureMouse();
		}

		/// <summary>
		///     Whether or not this control is currently being pressed by the left mouse button.
		/// </summary>
		public bool IsPressed
		{
			get { return (bool) GetValue(IsPressedProperty); }
			protected set { SetValue(IsPressedPropertyKey, value); }
		}

		/// <summary>
		///     The text that shall be displayed instead of the <see cref="NavigateUri" />.
		/// </summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary>
		///     The URI the browser should navigate to.
		/// </summary>
		public Uri NavigateUri
		{
			get { return (Uri) GetValue(NavigateUriProperty); }
			set { SetValue(NavigateUriProperty, value); }
		}
	}
}