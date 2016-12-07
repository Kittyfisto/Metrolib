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

		private bool _panning;
		private Point _mousePanPosition;

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

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (_panning && MousePanningMode == PanningMode.Both)
			{
				var position = e.GetPosition(this);
				var delta = _mousePanPosition - position;

				var x = HorizontalOffset + delta.X;
				var y = VerticalOffset + delta.Y;

				ScrollToHorizontalOffset(x);
				ScrollToVerticalOffset(y);

				_mousePanPosition = position;
			}
			else
			{
				base.OnMouseMove(e);
			}
		}

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