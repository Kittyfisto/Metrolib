using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A <see cref="TreeView" /> in the usual flat style:
	///     - Minimal borders
	///     - No parent-child indicator (children are merely indented)
	/// </summary>
	public class FlatTreeView
		: TreeView
	{
		static FlatTreeView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatTreeView), new FrameworkPropertyMetadata(typeof (FlatTreeView)));
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new FlatTreeViewItem();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FlatTreeViewItem;
		}
	}
}