using System;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Base class for a layer of a <see cref="MapView" />.
	/// </summary>
	/// <remarks>
	///     The most used layer is a <see cref="ItemsLayer" />.
	/// </remarks>
	public abstract class Layer
		: Canvas
	{
		private Camera _camera;

		internal Camera Camera
		{
			get => _camera;
			set
			{
				if (value == _camera)
					return;

				if (_camera != null)
				{
					_camera.Changed -= CameraOnChanged;
				}

				_camera = value;

				if (_camera != null)
				{
					_camera.Changed += CameraOnChanged;
				}

				EmitCameraChanged(value);
			}
		}

		private void CameraOnChanged(object sender, CameraChangedEventArgs args)
		{
			EmitCameraChanged(_camera);
		}

		/// <summary>
		/// 
		/// </summary>
		public event Action<Camera> CameraChanged;

		private void EmitCameraChanged(Camera value)
		{
			CameraChanged?.Invoke(value);
		}
	}
}