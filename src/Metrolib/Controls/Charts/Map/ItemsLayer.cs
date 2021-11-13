using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A layer capable of displaying a list of items in a <see cref="MapView" />.
	/// </summary>
	/// <remarks>
	///     You can specify a different <see cref="ItemType" /> to change the <see cref="ContentPresenter" />
	///     that presents the item. By default a <see cref="MapViewPointItem" /> is used.
	/// </remarks>
	/// <remarks>
	///     You can specify a <see cref="DataTemplate" /> or <see cref="DataTemplateSelector" /> to
	///     define how an item is to be displayed.
	/// </remarks>
	/// <remarks>
	///     If a <see cref="MaximumItemSize" /> is specified, the layer attempts to create <see cref="ContentPresenter" />
	///     for visible items only (what's known as virtualization)..
	/// </remarks>
	public class ItemsLayer
		: Layer
	{
		/// <summary>
		///     Definition of the <see cref="ItemType" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemTypeProperty =
			DependencyProperty.Register("ItemType", typeof (Type), typeof (ItemsLayer),
			                            new PropertyMetadata(typeof (MapViewPointItem)));

		/// <summary>
		///     Definition of the <see cref="MaximumItemSize" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty MaximumItemSizeProperty =
			DependencyProperty.Register("MaximumItemSize", typeof (Size), typeof (ItemsLayer),
			                            new PropertyMetadata(default(Size)));

		private static readonly DependencyPropertyKey SupportsVirtualizationPropertyKey
			= DependencyProperty.RegisterReadOnly("SupportsVirtualization", typeof (bool), typeof (ItemsLayer),
			                                      new FrameworkPropertyMetadata(default(bool),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="SupportsVirtualization" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SupportsVirtualizationProperty
			= SupportsVirtualizationPropertyKey.DependencyProperty;

		/// <summary>
		///     Definition of the <see cref="ItemTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemTemplateProperty =
			DependencyProperty.Register("ItemTemplate", typeof (DataTemplate), typeof (ItemsLayer),
			                            new PropertyMetadata(default(DataTemplate)));

		/// <summary>
		///     Definition of the <see cref="ItemTemplateSelector" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemTemplateSelectorProperty =
			DependencyProperty.Register("ItemTemplateSelector", typeof (DataTemplateSelector), typeof (ItemsLayer),
			                            new PropertyMetadata(default(DataTemplateSelector)));

		/// <summary>
		///     Definition of the <see cref="ItemsSource" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty ItemsSourceProperty =
			DependencyProperty.Register("ItemsSource", typeof (IEnumerable), typeof (ItemsLayer),
			                            new PropertyMetadata(default(IEnumerable)));

		/// <summary>
		///     The type of <see cref="MapViewItem" /> that shall be used as <see cref="ContentPresenter" />.
		/// </summary>
		public Type ItemType
		{
			get { return (Type) GetValue(ItemTypeProperty); }
			set { SetValue(ItemTypeProperty, value); }
		}

		/// <summary>
		///     Whether or not this layer currently supports virtualization.
		/// </summary>
		public bool SupportsVirtualization
		{
			get { return (bool) GetValue(SupportsVirtualizationProperty); }
			protected set { SetValue(SupportsVirtualizationPropertyKey, value); }
		}

		/// <summary>
		///     The maximum size of an item.
		/// </summary>
		/// <remarks>
		///     If set to a non-zero value, then virtualization is enabled.
		/// </remarks>
		public Size MaximumItemSize
		{
			get { return (Size) GetValue(MaximumItemSizeProperty); }
			set { SetValue(MaximumItemSizeProperty, value); }
		}

		/// <summary>
		///     The data template selector, responsible for finding the template to represent an item.
		/// </summary>
		public DataTemplateSelector ItemTemplateSelector
		{
			get { return (DataTemplateSelector) GetValue(ItemTemplateSelectorProperty); }
			set { SetValue(ItemTemplateSelectorProperty, value); }
		}

		/// <summary>
		///     The data template to represent an item.
		/// </summary>
		public DataTemplate ItemTemplate
		{
			get { return (DataTemplate) GetValue(ItemTemplateProperty); }
			set { SetValue(ItemTemplateProperty, value); }
		}

		/// <summary>
		///     The items to display.
		/// </summary>
		public IEnumerable ItemsSource
		{
			get { return (IEnumerable) GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
	}
}