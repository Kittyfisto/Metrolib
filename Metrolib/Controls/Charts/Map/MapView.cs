using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	[TemplatePart(Name = "PART_Canvas", Type = typeof (MapCanvas))]
	public class MapView
		: Control
	{
		private static readonly DependencyPropertyKey LayersPropertyKey
			= DependencyProperty.RegisterReadOnly("Layers", typeof (LayerCollection), typeof (MapView),
			                                      new FrameworkPropertyMetadata(default(LayerCollection),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="Layers" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty LayersProperty
			= LayersPropertyKey.DependencyProperty;

		private MapCanvas _canvas;

		static MapView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (MapView), new FrameworkPropertyMetadata(typeof (MapView)));
		}

		/// <summary>
		///     Initializes this map view.
		/// </summary>
		public MapView()
		{
			Layers = new LayerCollection();
			Layers.CollectionChanged += LayersOnCollectionChanged;
		}

		/// <summary>
		///     The layers being displayed by this map view.
		/// </summary>
		public LayerCollection Layers
		{
			get { return (LayerCollection) GetValue(LayersProperty); }
			protected set { SetValue(LayersPropertyKey, value); }
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_canvas = (MapCanvas) GetTemplateChild("PART_Canvas");
		}

		private void LayersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			switch (args.Action)
			{
				case NotifyCollectionChangedAction.Add:
					AddLayers(args.NewStartingIndex, args.NewItems.Cast<Layer>());
					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Remove:
					break;
				case NotifyCollectionChangedAction.Replace:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
			}
		}

		private void AddLayers(int newStartingIndex, IEnumerable<Layer> newItems)
		{
			if (_canvas != null)
				_canvas.AddLayers(newStartingIndex, newItems);
		}
	}
}