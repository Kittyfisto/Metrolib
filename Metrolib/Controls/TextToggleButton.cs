using System.Windows;
using System.Windows.Controls.Primitives;

namespace Metrolib.Controls
{
	public class TextToggleButton : ToggleButton
	{
		static TextToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (TextToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (TextToggleButton)));
		}
	}
}