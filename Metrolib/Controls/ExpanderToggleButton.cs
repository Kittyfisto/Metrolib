using System.Windows;

namespace Metrolib.Controls
{
	/// <summary>
	///     A toggle button that is typically used to expand/contract an area of the UI.
	/// </summary>
	public class ExpanderToggleButton
		: ExtToggleButton
	{
		static ExpanderToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ExpanderToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (ExpanderToggleButton)));
		}
	}
}