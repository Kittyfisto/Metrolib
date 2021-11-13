using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button which can be used to download something.
	/// </summary>
	/// <remarks>
	///     Displays a downward arrow with a line beneath.
	/// </remarks>
	public class DownloadButton
		: FlatIconButton
	{
		static DownloadButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DownloadButton),
			                                         new FrameworkPropertyMetadata(typeof(DownloadButton)));
		}
	}
}