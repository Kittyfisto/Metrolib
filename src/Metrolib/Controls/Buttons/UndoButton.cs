using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button to undo something, for example a textbox change.
	/// </summary>
	/// <remarks>
	///     Displays an curved arrow to the left.
	/// </remarks>
	public class UndoButton
		: FlatIconButton
	{
		static UndoButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(UndoButton),
				new FrameworkPropertyMetadata(typeof(UndoButton)));
		}
	}
}
