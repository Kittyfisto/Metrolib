using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	/// <summary>
	///     TBD
	/// </summary>
	public class NotificationBadge : ContentControl
	{
		static NotificationBadge()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (NotificationBadge),
			                                         new FrameworkPropertyMetadata(typeof (NotificationBadge)));
		}
	}
}