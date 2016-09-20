using System.Windows;
using System.Windows.Controls.Primitives;

namespace Metrolib.Controls
{
	/// <summary>
	///     A toggle button that is typically used to expand/contract an area of the UI.
	/// </summary>
	public class ExpanderToggleButton
		: ToggleButton
	{
		public static readonly DependencyProperty IsInvertedProperty =
			DependencyProperty.Register("IsInverted", typeof(bool), typeof(ExpanderToggleButton),
			                            new PropertyMetadata(false));

		static ExpanderToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ExpanderToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (ExpanderToggleButton)));
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