using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespaces
{
	/// <summary>
	///     A toggle button in the style of a pin. When <see cref="ToggleButtonBase.IsChecked" /> is set to false, then
	///     the pin is crossed out (<see cref="Icons.PinOff"/>), otherwise not (<see cref="Icons.Pin"/>).
	/// </summary>
	public class PinToggleButton
		: ToggleButtonBase
	{
		static PinToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PinToggleButton),
			                                         new FrameworkPropertyMetadata(typeof(PinToggleButton)));
		}

		/// <summary>
		///    Definition of the <see cref="RotateWhenUnchecked"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty RotateWhenUncheckedProperty = DependencyProperty.Register(
		                                                "RotateWhenUnchecked", typeof(bool), typeof(PinToggleButton), new PropertyMetadata(default(bool)));

		/// <summary>
		///    When set to true, then this toggle button will display the <see cref="Icons.Pin"/> rotated by 90° clockwise when
		///    <see cref="ToggleButtonBase.IsChecked"/> is set to false (e.g. it will behave more like Visual Studio's Toggle Buttons).
		/// </summary>
		public bool RotateWhenUnchecked
		{
			get { return (bool) GetValue(RotateWhenUncheckedProperty); }
			set { SetValue(RotateWhenUncheckedProperty, value); }
		}
	}
}