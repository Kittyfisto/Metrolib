using System.Windows;
using System.Windows.Controls.Primitives;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A "flat" scrollbar that features a single bar only (no arrows, border, etc...)
	/// </summary>
	public class FlatScrollBar
		: ScrollBar
	{
		/// <summary>
		///     Definition of the <see cref="Thickness" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ThicknessProperty =
			DependencyProperty.Register("Thickness", typeof (double), typeof (FlatScrollBar),
			                            new PropertyMetadata(default(double)));

		static FlatScrollBar()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FlatScrollBar),
			                                         new FrameworkPropertyMetadata(typeof (FlatScrollBar)));
		}

		/// <summary>
		///     The thickness of the scrollbar.
		/// </summary>
		public double Thickness
		{
			get { return (double) GetValue(ThicknessProperty); }
			set { SetValue(ThicknessProperty, value); }
		}
	}
}