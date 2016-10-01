using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Base class for toggle buttons of this library.
	/// </summary>
	/// <remarks>
	/// When a <see cref="ButtonBase.ContextMenu"/> is attached to this button, it is automatically opened for as long as
	/// the button is checked. Similarly, when the contextmenu is closed, the button is automatically unchecked.
	/// </remarks>
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
		/// Initializes a <see cref="ToggleButtonBase"/>.
		/// </summary>
		public ToggleButtonBase()
		{
			Checked += OnChecked;
			ContextMenuOpening += OnContextMenuOpening;
			ContextMenuClosing += OnContextMenuClosing;
		}

		private void OnContextMenuOpening(object sender, ContextMenuEventArgs contextMenuEventArgs)
		{
			IsChecked = true;
		}

		private void OnContextMenuClosing(object sender, ContextMenuEventArgs contextMenuEventArgs)
		{
			IsChecked = false;
		}

		private void OnChecked(object sender, RoutedEventArgs routedEventArgs)
		{
			var menu = ContextMenu;
			if (menu != null)
			{
				if (IsChecked == true)
				{
					menu.Placement = PlacementMode.Bottom;
					menu.PlacementTarget = this;
					menu.IsOpen = true;
				}
				else
				{
					menu.IsOpen = false;
				}
				menu.Closed += ContextMenuOnClosed;
			}
		}

		private void ContextMenuOnClosed(object sender, RoutedEventArgs routedEventArgs)
		{
			var contextMenu = sender as ContextMenu;
			if (contextMenu != null)
			{
				contextMenu.Closed -= ContextMenuOnClosed;
				IsChecked = false;
			}
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