using System.Windows;
using System.Windows.Controls.Primitives;

namespace Metrolib.Controls
{
	/// <summary>
	///     An extended toggle button that offers an IsInverted property to control how the content shall be displayed.
	/// </summary>
	public class ExtToggleButton
		: ToggleButton
	{
		public static readonly DependencyProperty IsInvertedProperty =
			DependencyProperty.Register("IsInverted", typeof (bool), typeof (ExtToggleButton),
			                            new PropertyMetadata(false));

		static ExtToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ExtToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (ExtToggleButton)));
		}

		/// <summary>
		///     Whether or not the colors of this toggle button are inverted.
		///     Set to false by default.
		/// </summary>
		/// <remarks>
		///     When inverted, the chevron is drawn in white instead of black.
		/// </remarks>
		public bool IsInverted
		{
			get { return (bool) GetValue(IsInvertedProperty); }
			set { SetValue(IsInvertedProperty, value); }
		}
	}
}