using System.Windows;
using Metrolib.Controls.Base;

namespace Metrolib.Controls
{
	public class TextToggleButton : FlatToggleButton
	{
		static TextToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (TextToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (TextToggleButton)));
		}
	}
}