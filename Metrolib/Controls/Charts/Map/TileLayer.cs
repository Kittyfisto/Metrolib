using System;
using System.Windows;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	public class TileLayer
		: Layer
	{
		/// <summary>
		/// </summary>
		public static readonly DependencyProperty VisibleTilesProperty = DependencyProperty.Register(
		                                                                                             "VisibleTiles",
		                                                                                             typeof(TileRectangle),
		                                                                                             typeof(TileLayer),
		                                                                                             new
			                                                                                             PropertyMetadata(TileRectangle.Empty,
			                                                                                                              OnVisibleTilesChanged));

		/// <summary>
		/// </summary>
		public TileLayer()
		{
			CameraChanged += OnCameraChanged;
		}

		/// <summary>
		/// </summary>
		public TileRectangle VisibleTiles
		{
			get => (TileRectangle) GetValue(VisibleTilesProperty);
			set => SetValue(VisibleTilesProperty, value);
		}

		private static void OnVisibleTilesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((TileLayer) d).VisibleTilesChanged?.Invoke((TileRectangle) e.NewValue);
		}

		/// <summary>
		///     This event is fired whenever <see cref="VisibleTiles" /> is changed.
		/// </summary>
		public event Action<TileRectangle> VisibleTilesChanged;

		private void OnCameraChanged(Camera camera)
		{
			if (Camera != null)
				VisibleTiles = TileRectangle.CreateFrom(Camera.VisibleMercatorRectangle, (int) Math.Ceiling(Camera.ZoomLevel));
			else
				VisibleTiles = TileRectangle.Empty;
		}
	}
}