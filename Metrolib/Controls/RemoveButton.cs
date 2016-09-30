using System.Windows;

namespace Metrolib.Controls
{
	/// <summary>
	/// </summary>
	public class RemoveButton : FlatButton
	{
		static RemoveButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (RemoveButton), new FrameworkPropertyMetadata(typeof (RemoveButton)));
		}
	}
}