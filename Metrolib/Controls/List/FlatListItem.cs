using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.List
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class FlatListItem
		: ListViewItem
	{
		static FlatListItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatListItem), new FrameworkPropertyMetadata(typeof(FlatListItem)));
		}
	}
}
