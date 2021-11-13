using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A toggle button with a letter icon.
	///     The icon displays an opened letter when toggled.
	/// </summary>
	public class EmailToggleButton
		: FlatToggleButton
	{
		static EmailToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (EmailToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (EmailToggleButton)));
		}
	}
}