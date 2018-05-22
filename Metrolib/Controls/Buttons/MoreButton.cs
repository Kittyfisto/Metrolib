using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button that allows the user to show more content than is regularly visible, for example through
	///     a context-menu.
	/// </summary>
	/// <remarks>
	///     Displays three dots.
	/// </remarks>
	public class MoreButton
		: FlatButton
	{
		static MoreButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (MoreButton), new FrameworkPropertyMetadata(typeof (MoreButton)));
		}
	}
}