using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	public class NextButton
		: FlatButton
	{
		static NextButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(NextButton), new FrameworkPropertyMetadata(typeof(NextButton)));
		}
	}
}
