using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A simple button that allows for the exporting of data.
	/// </summary>
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