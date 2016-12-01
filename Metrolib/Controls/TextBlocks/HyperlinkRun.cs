using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	///     A run that is styled to look like a typical hyperlink.
	///     Text is blue, the mouse changes its cursor on mouse over and the text changes color when
	///     hovered/pressed.
	/// </summary>
	/// <remarks>
	///     Is used by the <see cref="OpenInNewHyperlink" /> control which is suitable for most cases.
	///     This run should be used when the hyperlink is required to be part of a larger text (just place
	///     is next to any other run).
	/// </remarks>
	public class HyperlinkRun
		: Run
	{
		/// <summary>
		///     Definition of the <see cref="NavigateUri" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NavigateUriProperty =
			DependencyProperty.Register("NavigateUri", typeof (Uri), typeof (HyperlinkRun), new PropertyMetadata(default(Uri)));

		/// <summary>
		///     Definition of the <see cref="ProcessName" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ProcessNameProperty =
			DependencyProperty.Register("ProcessName", typeof (string), typeof (HyperlinkRun),
			                            new PropertyMetadata(default(string)));

		/// <summary>
		///     Definition of the <see cref="ProcessStartArguments" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ProcessStartArgumentsProperty =
			DependencyProperty.Register("ProcessStartArguments", typeof (string), typeof (HyperlinkRun),
			                            new PropertyMetadata(default(string)));

		private static readonly DependencyPropertyKey IsPressedPropertyKey
			= DependencyProperty.RegisterReadOnly("IsPressed", typeof (bool), typeof (HyperlinkRun),
			                                      new FrameworkPropertyMetadata(false,
			                                                                    FrameworkPropertyMetadataOptions.None,
			                                                                    OnIsPressedChanged));

		/// <summary>
		///     Definition of the <see cref="IsPressed" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsPressedProperty
			= IsPressedPropertyKey.DependencyProperty;

		/// <summary>
		/// Initializes this run.
		/// </summary>
		public HyperlinkRun()
		{
			MouseLeftButtonUp += OnMouseLeftButtonUp;
			MouseLeftButtonDown += OnMouseLeftButtonDown;
			TouchDown += OnTouchDown;
			TouchUp += OnTouchUp;
			MouseEnter += OnMouseEnter;
			MouseLeave += OnMouseLeave;
			IsEnabledChanged += OnIsEnabledChanged;

			UpdateForeground();
		}

		/// <summary>
		///     The URI the browser should navigate to.
		/// </summary>
		public Uri NavigateUri
		{
			get { return (Uri) GetValue(NavigateUriProperty); }
			set { SetValue(NavigateUriProperty, value); }
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
		///     Whether or not this control is currently being pressed by the left mouse button
		///     or a touch gesture.
		/// </summary>
		public bool IsPressed
		{
			get { return (bool) GetValue(IsPressedProperty); }
			protected set { SetValue(IsPressedPropertyKey, value); }
		}

		private static void OnIsPressedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((HyperlinkRun) dependencyObject).OnIsPressedChanged((bool) args.NewValue);
		}

		private void OnIsPressedChanged(bool isPressed)
		{
			UpdateForeground();
		}

		private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			IsPressed = true;
			CaptureMouse();
		}

		private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (IsMouseCaptured)
			{
				ReleaseMouseCapture();
			}

			Navigate();
			e.Handled = true;
		}

		private void OnTouchDown(object sender, TouchEventArgs e)
		{
			IsPressed = true;
		}

		private void OnTouchUp(object sender, TouchEventArgs touchEventArgs)
		{
			IsPressed = false;
			Navigate();
		}

		private void OnMouseEnter(object sender, MouseEventArgs e)
		{
			Cursor = Cursors.Hand;
			UpdateForeground();
		}

		private void OnMouseLeave(object sender, MouseEventArgs e)
		{
			Cursor = null;
			UpdateForeground();
		}

		private void OnIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs args)
		{
			UpdateForeground();
		}

		private void Navigate()
		{
			Uri uri = NavigateUri;
			string processName = ProcessName;
			if (uri != null)
			{
				var args = new ProcessStartInfo(NavigateUri.ToString());
				Start(args);
			}
			else if (processName != null)
			{
				var args = new ProcessStartInfo(processName, ProcessStartArguments);
				Start(args);
			}
			else
			{
				
			}
		}

		private void Start(ProcessStartInfo info)
		{
			try
			{
				Process.Start(info);
			}
			catch (Exception)
			{
				
			}
		}

		private void UpdateForeground()
		{
			if (!IsEnabled)
			{
				Foreground = Constants.ForegroundBrushDisabled;
			}
			else if (IsPressed)
			{
				Foreground = Constants.ForegroundBrushPressed;
			}
			else if (IsMouseOver)
			{
				Foreground = Constants.ForegroundBrushHovered;
			}
			else
			{
				Foreground = Constants.ForegroundBrushAccent;
			}
		}
	}
}