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

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new FlatTabItem();
		}

		protected override bool IsItemItsOwnContainerOverride(object item)
		{
			return item is FlatTabItem;
		}
	}
}