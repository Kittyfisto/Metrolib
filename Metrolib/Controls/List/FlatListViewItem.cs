using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.List
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class FlatListViewItem
		: ListViewItem
	{
		static FlatListViewItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatListViewItem), new FrameworkPropertyMetadata(typeof(FlatListViewItem)));
		}
	}
}
