using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	/// <summary>
	///     A "flat" <see cref="TextBlock" /> that supports the <see cref="Properties.IsInvertedProperty" />.
	///     Shall be used for less important text than <see cref="FlatTextBlock" />.
	/// </summary>
	public class SubTextBlock
		: TextBlock
	{
		static SubTextBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (SubTextBlock), new FrameworkPropertyMetadata(typeof (SubTextBlock)));
		}
	}
}