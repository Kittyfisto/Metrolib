using System.Windows;
using System.Windows.Controls.Primitives;

namespace Metrolib
{
	public class TextToggleButton : ToggleButton
	{
		static TextToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TextToggleButton), new FrameworkPropertyMetadata(typeof(TextToggleButton)));
		}
	}
}
