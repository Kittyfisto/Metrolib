using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
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

		public static readonly DependencyProperty NormalForegroundProperty =
			DependencyProperty.Register("NormalForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty HoveredForegroundProperty =
			DependencyProperty.Register("HoveredForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty HoveredBackgroundProperty =
			DependencyProperty.Register("HoveredBackground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty PressedBackgroundProperty =
			DependencyProperty.Register("PressedBackground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		public static readonly DependencyProperty BorderRadiusProperty =
			DependencyProperty.Register("BorderRadius", typeof (double), typeof (FlatButton),
			                            new PropertyMetadata(default(double)));

		public static readonly DependencyProperty PressedForegroundProperty =
			DependencyProperty.Register("PressedForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		static FlatButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatButton),
			                                         new FrameworkPropertyMetadata(typeof (FlatButton)));
		}

		public Brush NormalForeground
		{
			get { return (Brush) GetValue(NormalForegroundProperty); }
			set { SetValue(NormalForegroundProperty, value); }
		}

		/// <summary>
		///     The foreground of this button when pressed.
		/// </summary>
		public Brush PressedForeground
		{
			get { return (Brush) GetValue(PressedForegroundProperty); }
			set { SetValue(PressedForegroundProperty, value); }
		}

		/// <summary>
		///     The radius of the border/background.
		/// </summary>
		public double BorderRadius
		{
			get { return (double) GetValue(BorderRadiusProperty); }
			set { SetValue(BorderRadiusProperty, value); }
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
		///     The foreground of this button when it's hovered by the mouse.
		/// </summary>
		public Brush HoveredForeground
		{
			get { return (Brush) GetValue(HoveredForegroundProperty); }
			set { SetValue(HoveredForegroundProperty, value); }
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