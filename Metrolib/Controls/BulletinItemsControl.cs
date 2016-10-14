using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Metrolib.Controls
{
	/// <summary>
	///     An items control that presents each item with a bulletin point next to it.
	/// </summary>
	public class BulletinItemsControl
		: Control
	{
		/// <summary>
		///     Definition of the <see cref="ItemsSource" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.Register("ItemsSource", typeof (IEnumerable), typeof (BulletinItemsControl),
			                            new PropertyMetadata(default(IEnumerable)));

		/// <summary>
		///     Definition of the <see cref="BulletinMargin" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty BulletinMarginProperty =
			DependencyProperty.Register("BulletinMargin", typeof (Thickness), typeof (BulletinItemsControl),
			                            new PropertyMetadata(default(Thickness)));

		static BulletinItemsControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (BulletinItemsControl),
			                                         new FrameworkPropertyMetadata(typeof (BulletinItemsControl)));
		}

		/// <summary>
		///     The margin of the bulletin that's left to the actual content.
		/// </summary>
		public Thickness BulletinMargin
		{
			get { return (Thickness) GetValue(BulletinMarginProperty); }
			set { SetValue(BulletinMarginProperty, value); }
		}

		/// <summary>
		///     The list of items to be presented.
		/// </summary>
		public IEnumerable ItemsSource
		{
			get { return (IEnumerable) GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
	}
}