using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A scroll viewer offering a "flat" view:
	///     - The scrollbar is situated on top of the client content instead of besides it
	/// </summary>
	public class FlatScrollViewer
		: ScrollViewer
	{
		/// <summary>
		///     Definition of the <see cref="ScrollBarThickness" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ScrollBarThicknessProperty =
			DependencyProperty.Register("ScrollBarThickness", typeof (double), typeof (FlatScrollViewer),
			                            new PropertyMetadata(default(double)));

		/// <summary>
		///     Definition of the <see cref="MousePanningMode" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty MousePanningModeProperty =
			DependencyProperty.Register("MousePanningMode", typeof (PanningMode), typeof (FlatScrollViewer),
			                            new PropertyMetadata(default(PanningMode)));

		private Point _mousePanPosition;
		private bool _panning;

		static FlatScrollViewer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatScrollViewer),
			                                         new FrameworkPropertyMetadata(typeof (FlatScrollViewer)));
		}

		/// <summary>
		///     Similar to <see cref="PanningMode" />, but controls how the scrollviewer reacts to mouse events.
		/// </summary>
		public PanningMode MousePanningMode
		{
			get { return (PanningMode) GetValue(MousePanningModeProperty); }
			set { SetValue(MousePanningModeProperty, value); }
		}

		/// <summary>
		///     The thickness of the horizontal and vertical scrollbars.
		/// </summary>
		public double ScrollBarThickness
		{
			get { return (double) GetValue(ScrollBarThicknessProperty); }
			set { SetValue(ScrollBarThicknessProperty, value); }
		}

		/// <summary>
		///     Invoked when an unhandled System.Windows.Input.Mouse.MouseDown attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left && MousePanningMode == PanningMode.Both)
			{
				_panning = CaptureMouse();
				_mousePanPosition = e.GetPosition(this);
			}
			else
			{
				base.OnMouseDown(e);
			}
		}

		/// <summary>
		/// Invoked when an unhandled System.Windows.Input.Mouse.MouseMove attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_panning && MousePanningMode == PanningMode.Both)
			{
				Point position = e.GetPosition(this);
				Vector delta = _mousePanPosition - position;

				double x = HorizontalOffset + delta.X;
				double y = VerticalOffset + delta.Y;

				ScrollToHorizontalOffset(x);
				ScrollToVerticalOffset(y);

				_mousePanPosition = position;
			}
			else
			{
				base.OnMouseMove(e);
			}
		}

		/// <summary>
		/// Invoked when an unhandled System.Windows.Input.Mouse.MouseUp routed event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseButtonEventArgs e)
		{
			if (_panning && MousePanningMode == PanningMode.Both)
			{
				ReleaseMouseCapture();
				_panning = false;
			}
			else
			{
				base.OnMouseUp(e);
			}
		}
	}
}