using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// </summary>
	[TemplatePart(Name = "PART_Canvas", Type = typeof(MapCanvas))]
	public class MapView
		: ItemsControl
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty CameraProperty = DependencyProperty.Register(
		                                                                                       "Camera", typeof(Camera),
		                                                                                       typeof(MapView),
		                                                                                       new PropertyMetadata(null,
		                                                                                                            OnCameraChanged));

		private MapCanvas _canvas;
		private Point? _mouseDragStart;
		private Point _lastMousePosition;
		private Point _lastMouseDragPosition;
		private bool _isLoaded;

		static MapView()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MapView), new FrameworkPropertyMetadata(typeof(MapView)));
		}

		/// <summary>
		///     Initializes this map view.
		/// </summary>
		public MapView()
		{
			Camera = new Camera();

			Loaded += OnLoaded;
			Unloaded += OnUnloaded;

			SizeChanged += OnSizeChanged;
			MouseDown += OnMouseDown;
			MouseEnter += OnMouseEnter;
			MouseMove += OnMouseMove;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_isLoaded = true;
			foreach (Layer layer in Items)
			{
				if (layer != null)
					layer.Camera = Camera;
			}
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			_isLoaded = false;
		}

		private void OnMouseEnter(object sender, MouseEventArgs args)
		{
			_lastMousePosition = args.GetPosition(this);
		}

		private void OnMouseDown(object sender, MouseButtonEventArgs args)
		{
			_mouseDragStart = args.GetPosition(this);
		}

		private void OnMouseMove(object sender, MouseEventArgs args)
		{
			var currentPosition = args.GetPosition(this);
			if (args.LeftButton == MouseButtonState.Pressed)
			{
				if (Camera != null)
				{
					var delta = currentPosition - _lastMousePosition;
					if (delta != new Vector(0, 0))
					{
						var currentViewPosition = Camera.ToView(Camera.Position);
						var newViewPosition = currentViewPosition + delta;
						var newPosition = Camera.ToScene(newViewPosition);

						Camera.Position = newPosition;
						Camera.Update();
					}
				}

				_lastMouseDragPosition = currentPosition;
			}

			_lastMousePosition = currentPosition;
		}

		/// <summary>
		/// 
		/// </summary>
		public Camera Camera
		{
			get => (Camera) GetValue(CameraProperty);
			set => SetValue(CameraProperty, value);
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_canvas = (MapCanvas) GetTemplateChild("PART_Canvas");
		}

		private void OnSizeChanged(object sender, SizeChangedEventArgs args)
		{
			Camera?.Resize(args.NewSize.Width, args.NewSize.Height);
		}

		private static void OnCameraChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((MapView) d).OnCameraChanged((Camera) e.NewValue);
		}

		private void OnCameraChanged(Camera camera)
		{
			camera?.Resize(ActualWidth, ActualHeight);

			foreach (Layer layer in Items)
			{
				if (layer != null)
					layer.Camera = camera;
			}
		}
	}
}