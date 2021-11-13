using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A button which primarily displays an icon.
	/// </summary>
	public class FlatIconButton
		: FlatButton
	{
		/// <summary>
		///     Definition of the <see cref="Icon" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
		                                                                                     "Icon", typeof(System.Windows.Media.Geometry), typeof(FlatIconButton),
		                                                                                     new PropertyMetadata(default(System.Windows.Media.Geometry)));

		static FlatIconButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatIconButton),
			                                         new FrameworkPropertyMetadata(typeof(FlatIconButton)));
		}

		/// <summary>
		///     The icon to display.
		/// </summary>
		public System.Windows.Media.Geometry Icon
		{
			get { return (System.Windows.Media.Geometry) GetValue(IconProperty); }
			set { SetValue(IconProperty, value); }
		}
	}
}