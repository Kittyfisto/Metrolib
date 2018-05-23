using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button that can be used to collapse all items in a tree.
	/// </summary>
	/// <remarks>
	///     Displays a minus sign in a box.
	/// </remarks>
	public class CollapseAllButton : FlatButton
	{
		static CollapseAllButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (CollapseAllButton), new FrameworkPropertyMetadata(typeof (CollapseAllButton)));
		}
	}
}