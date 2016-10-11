using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// A group box in a flat style:
	/// - No border
	/// - Header has increased font size
	/// </summary>
	public class FlatGroupBox
		: GroupBox
	{
		static FlatGroupBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatGroupBox), new FrameworkPropertyMetadata(typeof(FlatGroupBox)));
		}
	}
}
