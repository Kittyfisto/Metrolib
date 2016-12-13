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
			                            new PropertyMetadata(default(double)));

		/// <summary>
		///     Definition of the <see cref="OpenAngle" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OpenAngleProperty =
			DependencyProperty.Register("OpenAngle", typeof (double), typeof (PieChartSliceItem),
			                            new PropertyMetadata(default(double)));

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
		///     The start angle in radians of the circle segment with 0 being the top.
		/// </summary>
		public double StartAngle
		{
			get { return (double) GetValue(StartAngleProperty); }
			set { SetValue(StartAngleProperty, value); }
		}
	}
}