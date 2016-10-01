using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A toggle button that is meant to display more content when toggled, for example through a
	///     context menu.
	/// </summary>
	public sealed class MoreToggleButton
		: FlatToggleButton
	{
		static MoreToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MoreToggleButton), new FrameworkPropertyMetadata(typeof(MoreToggleButton)));
		}
	}
}