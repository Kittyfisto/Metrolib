using System.Windows;
using System.Windows.Controls.Primitives;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// A "flat" scrollbar that features a single bar only (no arrows, border, etc...)
	/// </summary>
	public class FlatScrollBar
		: ScrollBar
	{
		static FlatScrollBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatScrollBar), new FrameworkPropertyMetadata(typeof(FlatScrollBar)));
		}
	}
}