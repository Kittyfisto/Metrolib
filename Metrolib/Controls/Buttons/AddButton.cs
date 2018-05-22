using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button that can be used to add things (files, items to a list, etc...).
	/// </summary>
	/// <remarks>
	///     Displays a plus sign.
	/// </remarks>
	public class AddButton : FlatButton
	{
		static AddButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (AddButton), new FrameworkPropertyMetadata(typeof (AddButton)));
		}
	}
}