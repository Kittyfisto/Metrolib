using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespaces
{
	/// <summary>
	///     A toggle button in the <see cref="Icons.ToggleSwitch" /> / <see cref="Icons.ToggleSwitchOff"/> style.
	/// </summary>
	public class SwitchToggleButton
		: ToggleButtonBase
	{
		static SwitchToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchToggleButton),
			                                         new FrameworkPropertyMetadata(typeof(SwitchToggleButton)));
		}
	}
}