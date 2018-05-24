using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public class WebTileLayer
		: TileLayer
	{
		/// <summary>
		/// </summary>
		public static readonly DependencyProperty TileCacheProperty = DependencyProperty.Register(
		                                                                                          "TileCache",
		                                                                                          typeof(ITileCache),
		                                                                                          typeof(WebTileLayer),
		                                                                                          new PropertyMetadata(default
		                                                                                                               (ITileCache
		                                                                                                               )));

		private readonly ObservableCollection<TileControl> _tiles;
		private readonly Dictionary<Tile, TileControl> _tilesByPosition;

		/// <summary>
		/// </summary>
		public WebTileLayer()
		{
			VisibleTilesChanged += OnVisibleTilesChanged;

			TileCache = new TileDownloader();

			_tilesByPosition = new Dictionary<Tile, TileControl>();
			_tiles = new ObservableCollection<TileControl>();
		}

		/// <summary>
		/// </summary>
		public ITileCache TileCache
		{
			get => (ITileCache) GetValue(TileCacheProperty);
			set => SetValue(TileCacheProperty, value);
		}

		private void OnVisibleTilesChanged(TileRectangle visibleTiles)
		{
			foreach (var tile in visibleTiles)
				if (!_tilesByPosition.ContainsKey(tile))
				{
					var control = new TileControl
					{
						Position = tile,
						ImageTask = TileCache.GetTileAsync(tile)
					};
					_tilesByPosition.Add(tile, control);
					_tiles.Add(control);
				}

			for (var i = 0; i < _tiles.Count;)
			{
				var tile = _tiles[i];
				if (!visibleTiles.Contains(tile.Position))
				{
					_tiles.RemoveAt(i);
					_tilesByPosition.Remove(tile.Position);
				}
				else
				{
					++i;
				}
			}
		}
	}
}