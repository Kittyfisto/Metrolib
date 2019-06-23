using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A toggle button with a plugin icon.
	///     The icon displays four puzzle pieces.
	/// </summary>
	public class PluginToggleButton
		: FlatToggleButton
	{
		static PluginToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (PluginToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (PluginToggleButton)));
		}
	}
}
