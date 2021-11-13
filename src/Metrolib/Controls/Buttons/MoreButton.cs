using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button that allows the user to show more content than is regularly visible, for example through
	///     a context-menu.
	/// </summary>
	/// <remarks>
	///     Displays three dots.
	/// </remarks>
	public class MoreButton
		: FlatButton
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(
		                                                                                            "Orientation",
		                                                                                            typeof(Orientation),
		                                                                                            typeof(MoreButton),
		                                                                                            new
			                                                                                            PropertyMetadata(default
			                                                                                                             (
				                                                                                                             Orientation
			                                                                                                             )));

		static MoreButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MoreButton),
			                                         new FrameworkPropertyMetadata(typeof(MoreButton)));
		}

		/// <summary>
		///     The orientation of the dots, defaults to horizontal.
		/// </summary>
		public Orientation Orientation
		{
			get { return (Orientation) GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}
	}
}