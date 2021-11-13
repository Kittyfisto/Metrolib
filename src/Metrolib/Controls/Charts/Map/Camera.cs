using System;
using System.Diagnostics.Contracts;
using System.Windows;
using GeoVis;
using Metrolib.Geography;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The camera of a <see cref="MapView" /> that controls which portion of the map is visible.
	/// </summary>
	public sealed class Camera
	{
		private double _mercatorToViewScale;
		private Vector _translation;
		private System.Windows.Size _viewSize;
		private double _viewToMercatorScale;
		private MercatorRectangle _visibleMercatorRectangle;

		/// <summary>
		///     The current position of the camera ("center" of the map view).
		/// </summary>
		public MercatorLocation Position { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public double ZoomLevel { get; set; }

		/// <summary>
		///     Width and height of view space.
		/// </summary>
		public System.Windows.Size ViewSize
		{
			get => _viewSize;
			set => _viewSize = value;
		}

		/// <summary>
		///     Width of view space.
		/// </summary>
		public double Width
		{
			get => ViewSize.Width;
			set => _viewSize.Width = value;
		}

		/// <summary>
		///     Height of view space.
		/// </summary>
		public double Height
		{
			get => _viewSize.Height;
			set => _viewSize.Height = value;
		}

		/// <summary>
		///     Rectangle defining visible view space.
		/// </summary>
		public Rect VisibleViewRectangle => new Rect(0, 0, Width, Height);

		/// <summary>
		///     Rectangle definining visible portion of mercator space.
		/// </summary>
		public MercatorRectangle VisibleMercatorRectangle => _visibleMercatorRectangle;

		/// <summary>
		/// 
		/// </summary>
		public event EventHandler<CameraChangedEventArgs> Changed;

		private void EmitChanged()
		{
			Changed?.Invoke(this, new CameraChangedEventArgs());
		}

		/// <summary>
		/// 
		/// </summary>
		public void Update()
		{
			double oldMercatorToViewScale = _mercatorToViewScale;
			Vector oldTranslation = _translation;

			_mercatorToViewScale = 256*Math.Pow(2, ZoomLevel)/MercatorLocation.Width;

			var offset = new Vector
				{
					X = Width/_mercatorToViewScale/2,
					Y = -Height/_mercatorToViewScale/2
				};
			_translation = new Vector
				{
					X = -Position.X + offset.X,
					Y = -Position.Y + offset.Y
				};

			_viewToMercatorScale = 1/_mercatorToViewScale;
			_visibleMercatorRectangle = ToScene(VisibleViewRectangle);

			if (oldMercatorToViewScale != _mercatorToViewScale ||
			    oldTranslation != _translation)
			{
				EmitChanged();
			}
		}

		/// <summary>
		///     Transforms a point WGS 84 space to view space.
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		[Pure]
		public Point ToView(GeoLocation location)
		{
			var mercator = (MercatorLocation) location;
			Point point = ToView(mercator);
			return point;
		}

		/// <summary>
		///     Transforms a point from view space to mercator space.
		/// </summary>
		/// <param name="pos"></param>
		/// <returns></returns>
		[Pure]
		public MercatorLocation ToScene(Point pos)
		{
			var tmp = new MercatorLocation
				{
					X = pos.X*_viewToMercatorScale - _translation.X,
					Y = pos.Y*(-_viewToMercatorScale) - _translation.Y
				};
			return tmp;
		}

		/// <summary>
		///     Transform a rectangle from view space to mercator space.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		[Pure]
		public MercatorRectangle ToScene(Rect rect)
		{
			MercatorLocation topLeft = ToScene(rect.TopLeft);
			MercatorLocation bottomRight = ToScene(rect.BottomRight);

			return new MercatorRectangle
				{
					Min = new MercatorLocation(Math.Min(topLeft.X, bottomRight.X), Math.Min(topLeft.Y, bottomRight.Y)),
					Max = new MercatorLocation(Math.Max(topLeft.X, bottomRight.X), Math.Max(topLeft.Y, bottomRight.Y))
				};
		}

		/// <summary>
		///     Transforms a point from mercator space to view space.
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		[Pure]
		public Point ToView(MercatorLocation location)
		{
			var point = new Point
				{
					X = Math.Floor((location.X + _translation.X)*_mercatorToViewScale + 0.5),
					Y = Math.Floor((location.Y + _translation.Y)*(-_mercatorToViewScale) + 0.5)
				};
			return point;
		}

		/// <summary>
		///     Transforms a rectangle from mercator space to view space.
		/// </summary>
		/// <param name="rect"></param>
		/// <returns></returns>
		[Pure]
		public Rect ToView(MercatorRectangle rect)
		{
			Point topLeft = ToView(new MercatorLocation(rect.Min.X, rect.Max.Y));
			Point bottomRight = ToView(new MercatorLocation(rect.Max.X, rect.Min.Y));

			double x = Math.Min(topLeft.X, bottomRight.X);
			double y = Math.Min(topLeft.Y, bottomRight.Y);
			double width = Math.Max(topLeft.X, bottomRight.X) - x;
			double height = Math.Max(topLeft.Y, bottomRight.Y) - y;

			return new Rect
				{
					X = x,
					Y = y,
					Width = width,
					Height = height
				};
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public void Resize(double width, double height)
		{
			Width = width;
			Height = height;
			Update();
		}
	}
}