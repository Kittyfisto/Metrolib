using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A button which can be used to close something (for example a window).
	/// </summary>
	/// <remarks>
	///     Displays a (rotated) cross.
	/// </remarks>
	public class CloseButton
		 : FlatIconButton
	{
		static CloseButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(CloseButton),
				new FrameworkPropertyMetadata(typeof(CloseButton)));
		}
	}
}