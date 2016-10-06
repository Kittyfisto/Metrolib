using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button to initiate a search.
	///     Displays a magnifying glass.
	/// </summary>
	public class SearchButton
		: FlatButton
	{
		static SearchButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (SearchButton), new FrameworkPropertyMetadata(typeof (SearchButton)));
		}
	}
}