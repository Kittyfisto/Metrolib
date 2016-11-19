using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls.Charts
{
	public class AxisControl
		: ContentControl
	{
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register("Orientation", typeof (Orientation), typeof (AxisControl),
			                            new PropertyMetadata(default(Orientation)));

		public static readonly DependencyProperty RangeProperty =
			DependencyProperty.Register("Range", typeof (Range), typeof (AxisControl), new PropertyMetadata(default(Range)));

		static AxisControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (AxisControl), new FrameworkPropertyMetadata(typeof (AxisControl)));
		}

		public Range Range
		{
			get { return (Range) GetValue(RangeProperty); }
			set { SetValue(RangeProperty, value); }
		}

		public Orientation Orientation
		{
			get { return (Orientation) GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
	}
}