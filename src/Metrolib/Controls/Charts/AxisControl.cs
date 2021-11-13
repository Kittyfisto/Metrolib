using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.Charts
{
	/// <summary>
	///     Responsible for displaying the vertical or horizontal axis of a chart.
	/// </summary>
	public class AxisControl
		: ContentControl
	{
		/// <summary>
		///     Definition of the <see cref="Orientation" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof (Orientation), typeof (AxisControl),
			                            new PropertyMetadata(default(Orientation)));

		/// <summary>
		///     Definition of the <see cref="Range" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty RangeProperty =
			DependencyProperty.Register("Range", typeof (Range), typeof (AxisControl), new PropertyMetadata(default(Range)));

		static AxisControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (AxisControl), new FrameworkPropertyMetadata(typeof (AxisControl)));
		}

		/// <summary>
		///     The range in values to be displayed on this axis.
		/// </summary>
		public Range Range
		{
			get { return (Range) GetValue(RangeProperty); }
			set { SetValue(RangeProperty, value); }
		}

		/// <summary>
		///     The orientation of this axis (vertical or horizontal).
		/// </summary>
		public Orientation Orientation
		{
			get { return (Orientation) GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
	}
}