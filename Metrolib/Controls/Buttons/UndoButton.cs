using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button to undo something, for example a textbox change.
	/// </summary>
	public class UndoButton
		: FlatButton
	{
		static UndoButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(UndoButton),
				new FrameworkPropertyMetadata(typeof(UndoButton)));
		}
	}
}
