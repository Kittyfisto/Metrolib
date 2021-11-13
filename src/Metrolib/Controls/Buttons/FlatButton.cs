using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The base class for most buttons offered by this library.
	/// </summary>
	public class FlatButton
		: Button
	{
		/// <summary>
		///     Definition of the <see cref="InvertedForeground" /> property.
		/// </summary>
		public static readonly DependencyProperty InvertedForegroundProperty =
			DependencyProperty.Register("InvertedForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		/// <summary>
		///     Definition of the <see cref="NormalForeground" /> property.
		/// </summary>
		public static readonly DependencyProperty NormalForegroundProperty =
			DependencyProperty.Register("NormalForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		/// <summary>
		///     Definition of the <see cref="HoveredForeground" /> property.
		/// </summary>
		public static readonly DependencyProperty HoveredForegroundProperty =
			DependencyProperty.Register("HoveredForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		/// <summary>
		///     Definition of the <see cref="HoveredBackground" /> property.
		/// </summary>
		public static readonly DependencyProperty HoveredBackgroundProperty =
			DependencyProperty.Register("HoveredBackground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		/// <summary>
		///     Definition of the <see cref="PressedBackground" /> property.
		/// </summary>
		public static readonly DependencyProperty PressedBackgroundProperty =
			DependencyProperty.Register("PressedBackground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		/// <summary>
		///     Definition of the <see cref="BorderRadius" /> property.
		/// </summary>
		public static readonly DependencyProperty BorderRadiusProperty =
			DependencyProperty.Register("BorderRadius", typeof (double), typeof (FlatButton),
			                            new PropertyMetadata(default(double)));

		/// <summary>
		///     Definition of the <see cref="PressedForeground" /> property.
		/// </summary>
		public static readonly DependencyProperty PressedForegroundProperty =
			DependencyProperty.Register("PressedForeground", typeof (Brush), typeof (FlatButton),
			                            new PropertyMetadata(default(Brush)));

		static FlatButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatButton),
			                                         new FrameworkPropertyMetadata(typeof (FlatButton)));
		}

		/// <summary>
		///     Initializes this button.
		/// </summary>
		public FlatButton()
		{
			Click += OnClick;
		}

		/// <summary>
		///     The foreground brush that is used when the control is not in any of the following states:
		///     -<see cref="UIElement.IsMouseOver" />
		///     -<see cref="ButtonBase.IsPressed" />
		///     -<see cref="UIElement.IsEnabled" />
		/// </summary>
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
		///     The foreground color, used when <see cref="Properties.IsInvertedProperty" /> is set to true.
		/// </summary>
		public Brush InvertedForeground
		{
			get { return (Brush) GetValue(InvertedForegroundProperty); }
			set { SetValue(InvertedForegroundProperty, value); }
		}

		private void OnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			ContextMenu menu = ContextMenu;
			if (menu != null)
			{
				menu.PlacementTarget = this;
				menu.IsOpen = true;
			}
		}
	}
}