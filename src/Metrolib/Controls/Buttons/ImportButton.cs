using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A simple button that allows for the import of data.
	/// </summary>
	/// <remarks>
	///     Displays a file with an arrow to bottom right.
	/// </remarks>
	public class ImportButton
		: FlatIconButton
	{
		static ImportButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ImportButton),
			                                         new FrameworkPropertyMetadata(typeof (ImportButton)));
		}
	}
}