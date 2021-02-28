using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button that should spawn a folder open dialog or a new explorer folder.
	/// </summary>
	/// <remarks>
	///     Displays an open folder.
	/// </remarks>
	public class FolderOpenButton : FlatIconButton
	{
		static FolderOpenButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FolderOpenButton),
				new FrameworkPropertyMetadata(typeof(FolderOpenButton)));
		}
	}
}