using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button used to jump to the previous something, for example
	///     the previous occurence of a search term in a document.
	/// </summary>
	/// <remarks>
	///     Displays an upwards arrow.
	/// </remarks>
	public class PreviousButton
		: FlatButton
	{
		static PreviousButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (PreviousButton),
			                                         new FrameworkPropertyMetadata(typeof (PreviousButton)));
		}
	}
}