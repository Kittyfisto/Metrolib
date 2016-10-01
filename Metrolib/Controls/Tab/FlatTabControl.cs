using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.Tab
{
	public class FlatTabControl
		: TabControl
	{
		static FlatTabControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatTabControl), new FrameworkPropertyMetadata(typeof(FlatTabControl)));
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
