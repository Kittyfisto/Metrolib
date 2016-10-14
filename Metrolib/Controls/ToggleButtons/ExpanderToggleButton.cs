using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A toggle button that is typically used to expand/contract an area of the UI.
	/// </summary>
	public class ExpanderToggleButton
		: ToggleButton
	{
		/// <summary>
		///     Definition of the <see cref="InvertedForeground" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty InvertedForegroundProperty =
			DependencyProperty.Register("InvertedForeground", typeof (Brush), typeof (ExpanderToggleButton),
			                            new PropertyMetadata(default(Brush)));

		/// <summary>
		///     Definition of the <see cref="IconHeight" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IconHeightProperty =
			DependencyProperty.Register("IconHeight", typeof (double), typeof (ExpanderToggleButton),
			                            new PropertyMetadata(default(double)));

		static ExpanderToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ExpanderToggleButton),
			                                         new FrameworkPropertyMetadata(typeof (ExpanderToggleButton)));
		}

		/// <summary>
		///     The height of the icon.
		/// </summary>
		public double IconHeight
		{
			get { return (double) GetValue(IconHeightProperty); }
			set { SetValue(IconHeightProperty, value); }
		}

		/// <summary>
		///     The foreground color, used when <see cref="Properties.IsInvertedProperty" /> is set to true.
		/// </summary>
		public Brush InvertedForeground
		{
			get { return (Brush) GetValue(InvertedForegroundProperty); }
			set { SetValue(InvertedForegroundProperty, value); }
		}
	}
}