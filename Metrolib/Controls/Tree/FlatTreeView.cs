using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
		/// <summary>
		///     Definition of the <see cref="IsExpandable" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsExpandableProperty =
			DependencyProperty.Register("IsExpandable", typeof (bool), typeof (FlatTreeView), new PropertyMetadata(true));

		static FlatTreeView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatTreeView), new FrameworkPropertyMetadata(typeof (FlatTreeView)));
		}

		/// <summary>
		///     Whether or not items in this tree can be expanded.
		///     If they can be, then a clickable arrow appears next to each item's content.
		///     If not, then this arrow is hidden.
		/// </summary>
		public bool IsExpandable
		{
			get { return (bool) GetValue(IsExpandableProperty); }
			set { SetValue(IsExpandableProperty, value); }
		}

		/// <summary>
		///     Creates and returns a new Metrolib.Controls.FlatTreeViewItem container.
		/// </summary>
		/// <returns></returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			var item = new FlatTreeViewItem();
			var binding = new Binding("IsExpandable")
				{
					Source = this
				};
			BindingOperations.SetBinding(item, FlatTreeViewItem.IsExpandableProperty, binding);
			return item;
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