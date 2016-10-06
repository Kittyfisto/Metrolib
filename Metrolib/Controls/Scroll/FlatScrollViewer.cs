using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A scroll viewer offering a "flat" view:
	///     - The scrollbar is situated on top of the client content instead of besides it
	/// </summary>
	public class FlatScrollViewer
		: ScrollViewer
	{
		static FlatScrollViewer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatScrollViewer),
			                                         new FrameworkPropertyMetadata(typeof (FlatScrollViewer)));
		}
	}
}