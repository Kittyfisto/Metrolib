using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	public class PreviousButton
		: FlatButton
	{
		static PreviousButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PreviousButton), new FrameworkPropertyMetadata(typeof(PreviousButton)));
		}
	}
}