using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button to initiate a search.
	/// </summary>
	/// <remarks>
	///     Displays a magnifying glass.
	/// </remarks>
	public class SearchButton
		: FlatIconButton
	{
		static SearchButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (SearchButton), new FrameworkPropertyMetadata(typeof (SearchButton)));
		}
	}
}