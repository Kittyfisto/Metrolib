using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.Tab
{
	/// <summary>
	///     A "flat" tab control.
	/// </summary>
	public class FlatTabControl
		: TabControl
	{
		static FlatTabControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatTabControl),
			                                         new FrameworkPropertyMetadata(typeof (FlatTabControl)));
		}

		/// <summary>
		///     Creates and returns a new Metrolib.Controls.FlatTabItem container.
		/// </summary>
		/// <returns></returns>
		protected override DependencyObject GetContainerForItemOverride()
		{
			return new FlatTabItem();
		}

		/// <summary>
		///     Determines whether an object is a Metrolib.Controls.FlatTabItem.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FlatTabItem;
		}
	}
}