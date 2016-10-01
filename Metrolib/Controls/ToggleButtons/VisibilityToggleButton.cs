using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A toggle button that allows the user to control whether a filter is being used to include or exclude items.
	/// </summary>
	public class VisibilityToggleButton
		: FlatToggleButton
	{
		static VisibilityToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (VisibilityToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (VisibilityToggleButton)));
		}
	}
}