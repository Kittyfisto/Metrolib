using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	public class NotificationBadge : ContentControl
	{
		static NotificationBadge()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NotificationBadge),
			                                         new FrameworkPropertyMetadata(typeof (NotificationBadge)));
		}
	}
}