using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button to represent an unorderly layout.
	/// </summary>
	public class ViewQuiltToggleButton
		: FlatToggleButton
	{
		static ViewQuiltToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewQuiltToggleButton),
				new FrameworkPropertyMetadata(typeof(ViewQuiltToggleButton)));
		}
	}
}