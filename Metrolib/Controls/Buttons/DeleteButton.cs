using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button which can be used to delete something for good.
	/// </summary>
	/// <remarks>
	///     Displays a trashcan.
	/// </remarks>
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