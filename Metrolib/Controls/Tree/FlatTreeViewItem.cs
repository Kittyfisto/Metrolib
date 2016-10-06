using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public class FlatTreeViewItem
		: TreeViewItem
	{
		static FlatTreeViewItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatTreeViewItem),
			                                         new FrameworkPropertyMetadata(typeof (FlatTreeViewItem)));
		}

		/// <summary>
		///     The depth of this item.
		///     Items that are children of their <see cref="FlatTreeView" /> have a depth of 0.
		/// </summary>
		public int Depth
		{
			get
			{
				int depth = 0;
				FlatTreeViewItem item = this;
				while (item != null)
				{
					item = item.ParentItem;
					++depth;
				}
				return depth - 1;
			}
		}

		private FlatTreeViewItem ParentItem
		{
			get
			{
				DependencyObject dependencyObject = this;
				do
				{
					dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
				} while (!(dependencyObject is FlatTreeViewItem || dependencyObject is FlatTreeView));
				return dependencyObject as FlatTreeViewItem;
			}
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