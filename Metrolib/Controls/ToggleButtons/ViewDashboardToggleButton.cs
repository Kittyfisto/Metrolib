using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button to represent an ordered layout.
	/// </summary>
	public class ViewDashboardToggleButton
		: FlatToggleButton
	{
		static ViewDashboardToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ViewDashboardToggleButton),
				new FrameworkPropertyMetadata(typeof(ViewDashboardToggleButton)));
		}
	}
}