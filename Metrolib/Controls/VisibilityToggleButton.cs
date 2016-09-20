using System.Windows;

namespace Metrolib.Controls
{
	/// <summary>
	///     A toggle button that allows the user to control whether a filter is being used to include or exclude items.
	/// </summary>
	public class VisibilityToggleButton
		: ExtToggleButton
	{
		static VisibilityToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (VisibilityToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (VisibilityToggleButton)));
		}
	}
}