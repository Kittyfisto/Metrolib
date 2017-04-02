using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	[TemplatePart(Name = "PART_PasswordBox", Type = typeof (PasswordBox))]
	[TemplatePart(Name = "PART_Watermark", Type = typeof (TextBlock))]
	public class FlatPasswordBox
		: Control
	{
		/// <summary>
		///     Definition of the <see cref="Watermark" /> property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty =
			DependencyProperty.Register("Watermark", typeof (string), typeof (FlatPasswordBox),
			                            new PropertyMetadata(default(string)));

		/// <summary>
		///     Definition of the <see cref="Password" /> property.
		/// </summary>
		public static readonly DependencyProperty PasswordProperty =
			DependencyProperty.Register("Password", typeof(string), typeof(FlatPasswordBox),
										new PropertyMetadata(default(string), OnPasswordChanged));

		private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((FlatPasswordBox) d).OnPasswordChanged((string)e.NewValue);
		}

		private void OnPasswordChanged(string value)
		{
			if (_passwordBox != null)
			{
				_passwordBox.Password = value;
				UpdateWatermarkVisibility();

				PasswordChanged?.Invoke(this, new RoutedEventArgs());
			}
		}

		private PasswordBox _passwordBox;
		private TextBlock _watermark;

		static FlatPasswordBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatPasswordBox),
			                                         new FrameworkPropertyMetadata(typeof (FlatPasswordBox)));
		}

		/// <summary>
		///     The watermark that is displayed for as long as no password has been entered.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		/// <summary>
		///     The password that has been entered by the user.
		/// </summary>
		public string Password
		{
			get
			{
				return (string) GetValue(PasswordProperty);
			}
			set
			{
				SetValue(PasswordProperty, value);
			}
		}

		/// <summary>
		///     Is fired whenever the <see cref="Password" /> changes.
		/// </summary>
		public event RoutedEventHandler PasswordChanged;

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (_passwordBox != null)
			{
				_passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;
			}

			_passwordBox = (PasswordBox) GetTemplateChild("PART_PasswordBox");
			if (_passwordBox != null)
			{
				_passwordBox.Password = Password;
				_passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
			}

			_watermark = (TextBlock) GetTemplateChild("PART_Watermark");
		}

		private void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs routedEventArgs)
		{
			if (_passwordBox != null)
				Password = _passwordBox.Password;

			PasswordChanged?.Invoke(this, new RoutedEventArgs());
		}

		private void UpdateWatermarkVisibility()
		{
			_watermark.Visibility = string.IsNullOrEmpty(Password)
				                        ? Visibility.Visible
				                        : Visibility.Hidden;
		}
	}
}