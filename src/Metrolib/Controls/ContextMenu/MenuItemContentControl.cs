using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// 
	/// </summary>
	public class MenuItemContentControl
		: ContentControl
	{
		static MenuItemContentControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (MenuItemContentControl),
			                                         new FrameworkPropertyMetadata(typeof (MenuItemContentControl)));
		}
	}
}