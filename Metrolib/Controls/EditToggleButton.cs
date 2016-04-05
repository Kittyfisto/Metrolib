using System.Windows;
using System.Windows.Controls.Primitives;

namespace Metrolib.Controls
{
	public class EditToggleButton : ToggleButton
	{
		static EditToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(EditToggleButton), new FrameworkPropertyMetadata(typeof(EditToggleButton)));
		}
	}
}
