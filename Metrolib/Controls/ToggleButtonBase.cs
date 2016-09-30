using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Metrolib.Controls
{
	/// <summary>
	///     Base class for toggle buttons of this library.
	/// </summary>
	public class ToggleButtonBase
		: ToggleButton
	{
		public static readonly DependencyProperty InvertedForegroundProperty =
			DependencyProperty.Register("InvertedForeground", typeof (Brush), typeof (ToggleButtonBase),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty HoveredBackgroundProperty =
			DependencyProperty.Register("HoveredBackground", typeof (Brush), typeof (ToggleButtonBase),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty PressedBackgroundProperty =
			DependencyProperty.Register("PressedBackground", typeof (Brush), typeof (ToggleButtonBase),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty CheckedBackgroundProperty =
			DependencyProperty.Register("CheckedBackground", typeof (Brush), typeof (ToggleButtonBase),
			                            new PropertyMetadata(default(Brush)));

		static ToggleButtonBase()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (ToggleButtonBase),
			                                         new FrameworkPropertyMetadata(typeof (ToggleButtonBase)));
		}

		/// <summary>
		///     The background of this button when it's checked.
		/// </summary>
		public Brush CheckedBackground
		{
			get { return (Brush) GetValue(CheckedBackgroundProperty); }
			set { SetValue(CheckedBackgroundProperty, value); }
		}

		/// <summary>
		///     The background of this button when it's pressed by the mouse or by touch.
		/// </summary>
		public Brush PressedBackground
		{
			get { return (Brush) GetValue(PressedBackgroundProperty); }
			set { SetValue(PressedBackgroundProperty, value); }
		}

		/// <summary>
		///     The background of this button when it's hovered by the mouse.
		/// </summary>
		public Brush HoveredBackground
		{
			get { return (Brush) GetValue(HoveredBackgroundProperty); }
			set { SetValue(HoveredBackgroundProperty, value); }
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