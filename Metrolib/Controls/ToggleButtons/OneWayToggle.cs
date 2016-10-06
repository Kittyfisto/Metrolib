using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button that can only be toggled on, but not off by itself.
	///     Should be used in conjunction with at least one other <see cref="OneWayToggle" /> to allow
	///     the user to switch between two or more states (identical to a group of <see cref="RadioButton" />s).
	/// </summary>
	public class OneWayToggle : ContentControl
	{
		/// <summary>
		///     Definition of the <see cref="HasLeftBorder" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HasLeftBorderProperty =
			DependencyProperty.Register("HasLeftBorder", typeof (bool), typeof (OneWayToggle), new PropertyMetadata(true));

		/// <summary>
		///     Definition of the <see cref="HasRightBorder" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HasRightBorderProperty =
			DependencyProperty.Register("HasRightBorder", typeof (bool), typeof (OneWayToggle), new PropertyMetadata(true));

		/// <summary>
		///     Definition of the <see cref="IsPressed" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsPressedProperty =
			DependencyProperty.Register("IsPressed", typeof (bool), typeof (OneWayToggle), new PropertyMetadata(default(bool)));

		/// <summary>
		///     Definition of the <see cref="IsChecked" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsCheckedProperty =
			DependencyProperty.Register("IsChecked", typeof (bool), typeof (OneWayToggle), new PropertyMetadata(default(bool)));

		static OneWayToggle()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (OneWayToggle), new FrameworkPropertyMetadata(typeof (OneWayToggle)));
		}

		/// <summary>
		///     Whether or not this toggle button is checked.
		/// </summary>
		public bool IsChecked
		{
			get { return (bool) GetValue(IsCheckedProperty); }
			set { SetValue(IsCheckedProperty, value); }
		}

		/// <summary>
		///     Whether or not this toggle button is currently pressed (mouse/touch).
		/// </summary>
		public bool IsPressed
		{
			get { return (bool) GetValue(IsPressedProperty); }
			set { SetValue(IsPressedProperty, value); }
		}

		/// <summary>
		///     Whether or not this toggle button shall display a right-hand border.
		/// </summary>
		public bool HasRightBorder
		{
			get { return (bool) GetValue(HasRightBorderProperty); }
			set { SetValue(HasRightBorderProperty, value); }
		}

		/// <summary>
		///     Whether or not this toggle button shall display a left-hand border.
		/// </summary>
		public bool HasLeftBorder
		{
			get { return (bool) GetValue(HasLeftBorderProperty); }
			set { SetValue(HasLeftBorderProperty, value); }
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			e.Handled = true;
			IsPressed = true;
			CaptureMouse();

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			e.Handled = true;
			IsPressed = false;
			IsChecked = true;
			ReleaseMouseCapture();

			base.OnMouseUp(e);
		}
	}
}