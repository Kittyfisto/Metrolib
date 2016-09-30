using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Metrolib.Controls.Base
{
	/// <summary>
	///     The base class for any button offered by this library.
	/// </summary>
	public class FlatButton
		: Button
	{
		public static readonly DependencyProperty InvertedForegroundProperty =
			DependencyProperty.Register("InvertedForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty HoveredBackgroundProperty =
			DependencyProperty.Register("HoveredBackground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty PressedBackgroundProperty =
			DependencyProperty.Register("PressedBackground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		static FlatButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatButton),
			                                         new FrameworkPropertyMetadata(typeof (FlatButton)));
		}

		/// <summary>
		///     The background of this button when it's pressed by the mouse or a touch gesture.
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
		///     The foreground color, used when <see cref="IsInverted" /> is set to true.
		/// </summary>
		public Brush InvertedForeground
		{
			get { return (Brush) GetValue(InvertedForegroundProperty); }
			set { SetValue(InvertedForegroundProperty, value); }
		}
	}
}