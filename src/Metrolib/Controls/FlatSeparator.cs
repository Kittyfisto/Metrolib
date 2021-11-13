using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	/// <summary>
	/// </summary>
	public class FlatSeparator
		: Separator
	{
		/// <summary>
		///     Definition of the <see cref="Orientation" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
		                                                "Orientation", typeof(Orientation), typeof(FlatSeparator), new PropertyMetadata(Orientation.Horizontal));

		/// <summary>
		///    The visual orientation of this separator.
		/// </summary>
		public Orientation Orientation
		{
			get { return (Orientation) GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		static FlatSeparator()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatSeparator), new FrameworkPropertyMetadata(typeof(FlatSeparator)));
		}
	}
}