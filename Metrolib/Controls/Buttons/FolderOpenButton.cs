using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button that should spawn a folder open dialog or a new explorer folder.
	/// </summary>
	public class FolderOpenButton : FlatButton
	{
		static FolderOpenButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FolderOpenButton),
				new FrameworkPropertyMetadata(typeof(FolderOpenButton)));
		}
	}
}