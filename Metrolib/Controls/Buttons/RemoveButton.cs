using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button used to remove something, for example an item in a list.
	/// </summary>
	/// <remarks>
	///     Displays a minus sign.
	/// </remarks>
	public class RemoveButton : FlatButton
	{
		static RemoveButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (RemoveButton), new FrameworkPropertyMetadata(typeof (RemoveButton)));
		}
	}
}