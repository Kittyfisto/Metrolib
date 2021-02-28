using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button used to jump to the next something, for example
	///     the next occurrence of a search term in a document.
	/// </summary>
	/// <remarks>
	///     Displays a downwards arrow.
	/// </remarks>
	public class NextButton
		: FlatIconButton
	{
		static NextButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NextButton), new FrameworkPropertyMetadata(typeof (NextButton)));
		}
	}
}