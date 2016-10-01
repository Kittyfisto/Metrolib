using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
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