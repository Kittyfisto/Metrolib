using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A simple button that allows for adding folders.
	/// </summary>
	/// <remarks>
	///     Displays a folder with a plus.
	/// </remarks>
	public class FolderAddButton
		: FlatButton
	{
		static FolderAddButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FolderAddButton),
			                                         new FrameworkPropertyMetadata(typeof (FolderAddButton)));
		}
	}
}