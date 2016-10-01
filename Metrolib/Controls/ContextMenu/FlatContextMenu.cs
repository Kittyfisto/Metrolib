using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// A context menu that is styled in the typical flat look.
	/// </summary>
	public class FlatContextMenu
		: ContextMenu
	{
		static FlatContextMenu()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatContextMenu), new FrameworkPropertyMetadata(typeof(FlatContextMenu)));
		}
	}
}