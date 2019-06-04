using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A simple button that allows for the export of data.
	/// </summary>
	/// <remarks>
	///     Displays a file with an arrow to top right.
	/// </remarks>
	public class ExportButton
		: FlatButton
	{
		static ExportButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ExportButton),
			                                         new FrameworkPropertyMetadata(typeof (ExportButton)));
		}
	}
}