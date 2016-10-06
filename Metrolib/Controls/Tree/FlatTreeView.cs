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

		/// <summary>
		///     Creates and returns a new Metrolib.Controls.FlatTreeViewItem container.
		/// </summary>
		/// <returns></returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new FlatTreeViewItem();
		}

		/// <summary>
		///     Determines whether an object is a Metrolib.Controls.FlatTreeViewItem.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FlatTreeViewItem;
		}
	}
}