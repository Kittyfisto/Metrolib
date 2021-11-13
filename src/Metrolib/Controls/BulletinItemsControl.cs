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

		/// <summary>
		///     Definition of the <see cref="ItemTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemTemplateProperty =
			DependencyProperty.Register("ItemTemplate", typeof (DataTemplate), typeof (BulletinItemsControl),
			                            new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		///     Definition of the <see cref="ItemTemplateSelector" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemTemplateSelectorProperty =
			DependencyProperty.Register("ItemTemplateSelector", typeof (DataTemplateSelector), typeof (BulletinItemsControl),
			                            new PropertyMetadata(default(DataTemplateSelector)));

		static BulletinItemsControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (BulletinItemsControl),
			                                         new FrameworkPropertyMetadata(typeof (BulletinItemsControl)));
		}

		/// <summary>
		///     The template selector that is used to decide which template shall be used for each item of <see cref="ItemsSource" />.
		/// </summary>
		public DataTemplateSelector ItemTemplateSelector
		{
			get { return (DataTemplateSelector) GetValue(ItemTemplateSelectorProperty); }
			set { SetValue(ItemTemplateSelectorProperty, value); }
		}

		/// <summary>
		///     The template that is used to present each item of the <see cref="ItemsSource" />.
		/// </summary>
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate) GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
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