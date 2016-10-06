using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A "flat" tab item.
	/// </summary>
	public class FlatTabItem
		: TabItem
	{
		static FlatTabItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatTabItem), new FrameworkPropertyMetadata(typeof (FlatTabItem)));
		}
	}
}