using System.Windows;
using Metrolib.Controls.Base;

namespace Metrolib.Controls
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