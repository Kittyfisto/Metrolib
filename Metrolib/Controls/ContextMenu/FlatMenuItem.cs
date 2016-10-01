using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// 
	/// </summary>
	public class FlatMenuItem
		: MenuItem
	{
		static FlatMenuItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatMenuItem), new FrameworkPropertyMetadata(typeof(FlatMenuItem)));
		}
	}
}