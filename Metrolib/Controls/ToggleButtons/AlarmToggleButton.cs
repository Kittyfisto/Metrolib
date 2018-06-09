using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespaces
{
	/// <summary>
	///     A toggle button that allows an alarm to be on or off.
	/// </summary>
	public class AlarmToggleButton
		: ToggleButtonBase
	{
		static AlarmToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(AlarmToggleButton),
			                                         new FrameworkPropertyMetadata(typeof(AlarmToggleButton)));
		}
	}
}