using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Control representing a point on the map as a push pin.
	/// </summary>
	public class MapViewPointItem
		: MapViewItem
	{
		static MapViewPointItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (MapViewPointItem),
			                                         new FrameworkPropertyMetadata(typeof (MapViewPointItem)));
		}
	}
}