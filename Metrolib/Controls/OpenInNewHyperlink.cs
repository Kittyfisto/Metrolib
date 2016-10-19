using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	/// <summary>
	///     A control to display a hyperlink to a user supplied URI.
	///     Opens the default browser when clicked.
	/// </summary>
	public class OpenInNewHyperlink
		: Control
	{
		/// <summary>
		///     Definition of the <see cref="NavigateUri" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NavigateUriProperty =
			DependencyProperty.Register("NavigateUri", typeof (Uri), typeof (OpenInNewHyperlink),
			                            new PropertyMetadata(default(Uri)));

		/// <summary>
		///     Definition of the <see cref="Text" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof (string), typeof (OpenInNewHyperlink),
			                            new PropertyMetadata(default(string)));

		private static readonly DependencyPropertyKey IsPressedPropertyKey
			= DependencyProperty.RegisterReadOnly("IsPressed", typeof (bool), typeof (OpenInNewHyperlink),
			                                      new FrameworkPropertyMetadata(default(bool),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="ProcessName" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ProcessNameProperty =
			DependencyProperty.Register("ProcessName", typeof (string), typeof (OpenInNewHyperlink),
			                            new PropertyMetadata(default(string)));

		/// <summary>
		///     Definition of the <see cref="ProcessStartArguments" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ProcessStartArgumentsProperty =
			DependencyProperty.Register("ProcessStartArguments", typeof (string), typeof (OpenInNewHyperlink),
			                            new PropertyMetadata(default(string)));

		/// <summary>
		///     Definition of the <see cref="IsPressed" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsPressedProperty
			= IsPressedPropertyKey.DependencyProperty;

		static OpenInNewHyperlink()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (OpenInNewHyperlink),
			                                         new FrameworkPropertyMetadata(typeof (OpenInNewHyperlink)));
		}

		/// <summary>
		///     The process that shall be started via <see cref="Process.Start()" />.
		/// </summary>
		public string ProcessName
		{
			get { return (string) GetValue(ProcessNameProperty); }
			set { SetValue(ProcessNameProperty, value); }
		}

		/// <summary>
		///     The arguments that shall be passed to <see cref="Process.Start()" />.
		/// </summary>
		public string ProcessStartArguments
		{
			get { return (string) GetValue(ProcessStartArgumentsProperty); }
			set { SetValue(ProcessStartArgumentsProperty, value); }
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