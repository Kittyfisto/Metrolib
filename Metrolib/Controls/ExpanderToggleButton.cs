using System.Windows;
using Metrolib.Controls.Base;

namespace Metrolib.Controls
{
	/// <summary>
	///     A toggle button that is typically used to expand/contract an area of the UI.
	/// </summary>
	public class ExpanderToggleButton
		: FlatToggleButton
	{
		static ExpanderToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ExpanderToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (ExpanderToggleButton)));
		}
	}
}