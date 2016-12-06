using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.Charts.Network
{
	/// <summary>
	///     The content control that represents one node in <see cref="NetworkView.Nodes" />.
	/// </summary>
	public class NetworkViewNodeItem
		: ContentControl
	{
		static NetworkViewNodeItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NetworkViewNodeItem),
			                                         new FrameworkPropertyMetadata(typeof (NetworkViewNodeItem)));
		}

		public Point Position { get; set; }
		public Point DisplayPosition { get; set; }
	}
}