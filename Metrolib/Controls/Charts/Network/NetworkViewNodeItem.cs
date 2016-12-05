using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.Charts.Network
{
	/// <summary>
	///     The item representing an individual node in a <see cref="NetworkView" />.
	/// </summary>
	public class NetworkViewNodeItem
		: ContentControl
	{
		static NetworkViewNodeItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NetworkViewNodeItem), new FrameworkPropertyMetadata(typeof(NetworkViewNodeItem)));
		}
	}
}