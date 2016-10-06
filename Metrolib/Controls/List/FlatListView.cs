using System.Windows;
using System.Windows.Controls;
using Metrolib.Controls.List;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A list-view in a "flat" look:
	///     - no borders
	///     - the scrollbar(s) are hidden unless the list view is hovered
	///     - scrollbars are translucent and are on top of the client content
	/// </summary>
	public class FlatListView
		: ListView
	{
		static FlatListView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatListView), new FrameworkPropertyMetadata(typeof (FlatListView)));
		}

		/// <summary>
		///     Creates and returns a new Metrolib.Controls.FlatListViewItem container.
		/// </summary>
		/// <returns></returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new FlatListViewItem();
		}

		/// <summary>
		///     Determines whether an object is a Metrolib.Controls.FlatListViewItem.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FlatListViewItem;
		}
	}
}