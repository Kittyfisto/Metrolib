using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button that can be used to delete things (files, items to a list, etc...).
	/// </summary>
	/// <remarks>
	///     Displays a trashcan.
	/// </remarks>
	public class TrashButton : FlatButton
	{
		static TrashButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (TrashButton), new FrameworkPropertyMetadata(typeof (TrashButton)));
		}
	}
}