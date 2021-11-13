using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// A toggle button that allows to toggle between editing and not-.
	/// </summary>
	public class EditToggleButton : FlatToggleButton
	{
		static EditToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (EditToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (EditToggleButton)));
		}
	}
}