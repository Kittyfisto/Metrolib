using System.Windows;
using System.Windows.Controls;

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

		static FlatScrollViewer()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatScrollViewer),
			                                         new FrameworkPropertyMetadata(typeof (FlatScrollViewer)));
		}

		/// <summary>
		///     The thickness of the horizontal and vertical scrollbars.
		/// </summary>
		public double ScrollBarThickness
		{
			get { return (double) GetValue(ScrollBarThicknessProperty); }
			set { SetValue(ScrollBarThicknessProperty, value); }
		}
	}
}