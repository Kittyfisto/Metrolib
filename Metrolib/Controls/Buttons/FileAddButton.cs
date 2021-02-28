using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A simple button that allows for adding files.
	/// </summary>
	/// <remarks>
	///     Displays a file with a plus.
	/// </remarks>
	public class FileAddButton
		: FlatIconButton
	{
		static FileAddButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FileAddButton),
			                                         new FrameworkPropertyMetadata(typeof (FileAddButton)));
		}
	}
}