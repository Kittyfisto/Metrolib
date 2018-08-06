using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Metrolib.Controls.ToggleButtons;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public sealed class ItemsToggleButton
		: Control
	{
		/// <summary>
		/// </summary>
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
		                                                                                            "ItemsSource",
		                                                                                            typeof(IEnumerable),
		                                                                                            typeof(
			                                                                                            ItemsToggleButton
		                                                                                            ),
		                                                                                            new
			                                                                                            PropertyMetadata(null,
			                                                                                                             OnItemsSourceChanged));

		private static readonly DependencyPropertyKey ItemsPropertyKey
			= DependencyProperty.RegisterReadOnly("Items", typeof(IEnumerable<ToggleButtonItemViewModel>), typeof(ItemsToggleButton),
			                                      new FrameworkPropertyMetadata(default(IEnumerable<ToggleButtonItemViewModel>),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty ItemsProperty
			= ItemsPropertyKey.DependencyProperty;

		/// <summary>
		/// 
		/// </summary>
		public IEnumerable<ToggleButtonItemViewModel> Items
		{
			get => (IEnumerable<ToggleButtonItemViewModel>) GetValue(ItemsProperty);
			protected set => SetValue(ItemsPropertyKey, value);
		}

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
		                                                                                             "ItemTemplate",
		                                                                                             typeof(DataTemplate
		                                                                                             ),
		                                                                                             typeof(
			                                                                                             ItemsToggleButton
		                                                                                             ),
		                                                                                             new
			                                                                                             PropertyMetadata(default
			                                                                                                              (
				                                                                                                              DataTemplate
			                                                                                                              )));

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty ItemTemplateSelectorProperty = DependencyProperty.Register(
		                                                                                                     "ItemTemplateSelector",
		                                                                                                     typeof(
			                                                                                                     DataTemplateSelector
		                                                                                                     ),
		                                                                                                     typeof(
			                                                                                                     ItemsToggleButton
		                                                                                                     ),
		                                                                                                     new
			                                                                                                     PropertyMetadata(default
			                                                                                                                      (
				                                                                                                                      DataTemplateSelector
			                                                                                                                      )));

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
		                                                                                             "SelectedItem",
		                                                                                             typeof(object),
		                                                                                             typeof(
			                                                                                             ItemsToggleButton
		                                                                                             ),
		                                                                                             new
			                                                                                             PropertyMetadata(null, OnSelectedItemChanged));

		private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ItemsToggleButton) d).OnSelectedItemChanged(e.NewValue);
		}

		private void OnSelectedItemChanged(object newValue)
		{
			try
			{
				if (_changing)
					return;

				_changing = true;
				var viewModel = _items.FirstOrDefault(x => Equals(x.Item, newValue));
				foreach (var item in _items)
				{
					item.IsSelected = item == viewModel;
				}
			}
			finally
			{
				_changing = false;
			}
		}

		private readonly ObservableCollection<ToggleButtonItemViewModel> _items;
		private bool _changing;

		static ItemsToggleButton()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemsToggleButton),
			                                         new FrameworkPropertyMetadata(typeof(ItemsToggleButton)));
		}

		/// <summary>
		/// 
		/// </summary>
		public ItemsToggleButton()
		{
			Items = _items = new ObservableCollection<ToggleButtonItemViewModel>();
		}

		/// <summary>
		/// </summary>
		public DataTemplateSelector ItemTemplateSelector
		{
			get => (DataTemplateSelector) GetValue(ItemTemplateSelectorProperty);
			set => SetValue(ItemTemplateSelectorProperty, value);
		}

		/// <summary>
		/// </summary>
		public DataTemplate ItemTemplate
		{
			get => (DataTemplate) GetValue(ItemTemplateProperty);
			set => SetValue(ItemTemplateProperty, value);
		}

		/// <summary>
		/// </summary>
		public object SelectedItem
		{
			get => GetValue(SelectedItemProperty);
			set => SetValue(SelectedItemProperty, value);
		}

		/// <summary>
		/// </summary>
		public IEnumerable ItemsSource
		{
			get => (IEnumerable) GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ItemsToggleButton) d).OnItemsSourceChanged((IEnumerable) e.OldValue, (IEnumerable) e.NewValue);
		}

		private void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
		{
			var observable = oldValue as INotifyCollectionChanged;
			if (observable != null)
			{
				observable.CollectionChanged -= ItemsSourceOnCollectionChanged;
			}

			Clear();
			if (newValue != null)
				Add(newValue);

			observable = newValue as INotifyCollectionChanged;
			if (observable != null)
			{
				observable.CollectionChanged += ItemsSourceOnCollectionChanged;
			}
		}

		private void ItemsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Remove:
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Reset:
					Clear();
					Add(args.NewItems);
					break;
			}
		}

		private void Clear()
		{
			_items.Clear();
		}

		private void Add(IEnumerable items)
		{
			foreach (var item in items)
			{
				var viewModel = new ToggleButtonItemViewModel(item);
				viewModel.PropertyChanged += ViewModelOnPropertyChanged;
				_items.Add(viewModel);
			}
		}

		private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var viewModel = (ToggleButtonItemViewModel) sender;
			switch (e.PropertyName)
			{
				case nameof(ToggleButtonItemViewModel.IsSelected):
					if (viewModel.IsSelected)
					{
						SelectedItem = viewModel.Item;
						foreach (var otherViewModel in _items)
						{
							if (otherViewModel != viewModel)
								otherViewModel.IsSelected = false;
						}
					}
					break;
			}
		}
	}
}