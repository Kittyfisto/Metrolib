using System.Windows;

namespace Metrolib.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public class DeleteButton
		 : FlatButton
	{
		static DeleteButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DeleteButton),
				new FrameworkPropertyMetadata(typeof(DeleteButton)));
		}
	}
}