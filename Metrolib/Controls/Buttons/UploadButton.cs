using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button which can be used to upload something.
	/// </summary>
	/// <remarks>
	///     Displays an upward arrow with a line beneath.
	/// </remarks>
	public class UploadButton
		: FlatIconButton
	{
		static UploadButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(UploadButton),
			                                         new FrameworkPropertyMetadata(typeof(UploadButton)));
		}
	}
}