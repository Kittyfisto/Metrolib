using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A list of layers of a <see cref="MapView" />.
	/// </summary>
	public sealed class LayerCollection
		: ObservableCollection<Layer>
	{
		private Camera _camera;

		/// <summary>
		///     Initializes this object.
		/// </summary>
		public LayerCollection()
		{
			CollectionChanged += OnCollectionChanged;
		}

		public Camera Camera
		{
			get { return _camera; }
			set
			{
				if (value == _camera)
					return;

				_camera = value;
				foreach (Layer layer in this)
				{
					layer.Camera = value;
				}
			}
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
		{
			if (args.NewItems != null)
			{
				foreach (Layer layer in args.NewItems)
				{
					layer.Camera = _camera;
				}
			}

			if (args.OldItems != null)
			{
				foreach (Layer layer in args.OldItems)
				{
					layer.Camera = null;
				}
			}

			OnPropertyChanged(new PropertyChangedEventArgs("Count"));
		}
	}
}