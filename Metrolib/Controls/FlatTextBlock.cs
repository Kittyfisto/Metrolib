using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	/// <summary>
	///     A "flat" <see cref="TextBlock" /> that supports the <see cref="Properties.IsInvertedProperty" />.
	/// </summary>
	public class FlatTextBlock
		: TextBlock
	{
		static FlatTextBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatTextBlock), new FrameworkPropertyMetadata(typeof(FlatTextBlock)));
		}
	}
}