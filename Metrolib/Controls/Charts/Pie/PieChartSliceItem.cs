using System;
using System.Windows;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Responsible for drawing the actual slice that represents a <see cref="IPieSlice" />.
	/// </summary>
	public class PieChartSliceItem
		: FrameworkElement
	{
		/// <summary>
		///     Definition of the <see cref="StartAngle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty StartAngleProperty =
			DependencyProperty.Register("StartAngle", typeof (double), typeof (PieChartSliceItem),
			                            new PropertyMetadata(0.0, OnStartAngleChanged));

		/// <summary>
		///     Definition of the <see cref="Direction" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty DirectionProperty =
			DependencyProperty.Register("Direction", typeof (SliceDirection), typeof (PieChartSliceItem),
			                            new PropertyMetadata(SliceDirection.BottomLeft));

		/// <summary>
		///     Definition of the <see cref="OpenAngle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OpenAngleProperty =
			DependencyProperty.Register("OpenAngle", typeof (double), typeof (PieChartSliceItem),
			                            new PropertyMetadata(0.0, OnOpenAngleChanged));

		/// <summary>
		///     Definition of the <see cref="Slice" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SliceProperty =
			DependencyProperty.Register("Slice", typeof (IPieSlice), typeof (PieChartSliceItem),
			                            new PropertyMetadata(default(IPieSlice)));

		/// <summary>
		///     Definition of the <see cref="Radius" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty RadiusProperty =
			DependencyProperty.Register("Radius", typeof (double), typeof (PieChartSliceItem),
			                            new PropertyMetadata(default(double)));

		/// <summary>
		///     The direction the slice is pointing at, from the center of the pie chart.
		/// </summary>
		public SliceDirection Direction
		{
			get { return (SliceDirection) GetValue(DirectionProperty); }
			set { SetValue(DirectionProperty, value); }
		}

		/// <summary>
		///     The radius of this slice.
		/// </summary>
		public double Radius
		{
			get { return (double) GetValue(RadiusProperty); }
			set { SetValue(RadiusProperty, value); }
		}

		/// <summary>
		///     The slice being drawn.
		/// </summary>
		public IPieSlice Slice
		{
			get { return (IPieSlice) GetValue(SliceProperty); }
			set { SetValue(SliceProperty, value); }
		}

		/// <summary>
		///     The opening angle in radians of the circle segment.
		/// </summary>
		public double OpenAngle
		{
			get { return (double) GetValue(OpenAngleProperty); }
			set { SetValue(OpenAngleProperty, value); }
		}

		/// <summary>
		///     The start angle in radians of the circle segment with 0 being the bottom.
		/// </summary>
		public double StartAngle
		{
			get { return (double) GetValue(StartAngleProperty); }
			set { SetValue(StartAngleProperty, value); }
		}

		private static void OnOpenAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PieChartSliceItem) d).OnOpenAngleChanged((double) e.NewValue);
		}

		private void OnOpenAngleChanged(double newValue)
		{
			Direction = CalculateDirection(StartAngle, newValue);
		}

		private static void OnStartAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PieChartSliceItem) d).OnStartAngleChanged((double) e.NewValue);
		}

		private void OnStartAngleChanged(double newValue)
		{
			Direction = CalculateDirection(newValue, OpenAngle);
		}

		private static SliceDirection CalculateDirection(double startAngle, double openAngle)
		{
			double angle = startAngle + openAngle/2;

			if (angle >= 0 && angle < Math.PI/2)
			{
				return SliceDirection.BottomLeft;
			}
			if (angle >= Math.PI/2 && angle < Math.PI)
			{
				return SliceDirection.TopLeft;
			}
			if (angle >= Math.PI && angle < Math.PI*1.5)
			{
				return SliceDirection.TopRight;
			}
			return SliceDirection.BottomRight;
		}
	}
}