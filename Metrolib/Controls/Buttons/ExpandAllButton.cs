using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button that can be used to expand all items in a tree.
	/// </summary>
	/// <remarks>
	///     Displays a plus sign in a box.
	/// </remarks>
	public class ExpandAllButton : FlatButton
	{
		static ExpandAllButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ExpandAllButton), new FrameworkPropertyMetadata(typeof (ExpandAllButton)));
		}
	}
}