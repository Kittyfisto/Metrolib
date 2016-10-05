using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// 
	/// </summary>
	public class FlatTreeView
		: TreeView
	{
		static FlatTreeView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatTreeView), new FrameworkPropertyMetadata(typeof(FlatTreeView)));
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