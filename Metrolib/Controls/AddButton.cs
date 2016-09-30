using System.Windows;
using Metrolib.Controls.Base;

namespace Metrolib.Controls
{
	/// <summary>
	///     A button that can be used to add things (files, items to a list, etc...)
	/// </summary>
	public class AddButton : FlatButton
	{
		static AddButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (AddButton), new FrameworkPropertyMetadata(typeof (AddButton)));
		}
	}
}