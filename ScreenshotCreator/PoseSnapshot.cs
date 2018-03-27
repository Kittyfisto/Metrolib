using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ScreenshotCreator
{
	/// <summary>
	///     Allows capturing snapshots of an element in a defined pose (such as focused, disabled, etc...)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public sealed class PoseSnapshot<T>
		: IDisposable
		where T : FrameworkElement, new()
	{
		private readonly Dispatcher _dispatcher;
		private readonly T _element;
		private readonly string _pose;
		private readonly SnapshotCreator<T> _snapshotCreator;

		public PoseSnapshot(SnapshotCreator<T> snapshotCreator,
		                    Dispatcher dispatcher,
		                    ResourceDictionary resourceDictionary,
		                    string pose)
		{
			_snapshotCreator = snapshotCreator;
			_dispatcher = dispatcher;
			_pose = pose;
			_element = Invoke(() =>
			{
				var element = new T();
				return element;
			});
			Invoke(() =>
			{
				var style = (Style) resourceDictionary[typeof(T)];
				_element.Style = style;
			});
		}

		public void Resize(int width, int height)
		{
			Invoke(() =>
			{
				_element.Width = width;
				_element.Height = height;

				var size = new Size(width, height);
				_element.Measure(size);
				_element.Arrange(new Rect(new Point(), size));
			});
		}

		public void Prepare(Action<T> action)
		{
			Invoke(() => action(_element));
		}

		public void Focus()
		{
			Invoke(() =>
			{
				_element.Focusable = true;
				bool result = _element.Focus();
				Keyboard.Focus(_element);
			});
			Invoke(() =>
			{
				var keyboard = Keyboard.PrimaryDevice;
				var element = Keyboard.FocusedElement;
				bool isFocused = _element.IsFocused;
				bool isKeyboardFocus = _element.IsKeyboardFocused;
				bool isKeyboardFocusWithin = _element.IsKeyboardFocusWithin;
				int n = 0;
			});
		}

		public void Wait(TimeSpan timeout)
		{
			Thread.Sleep(timeout);
			Invoke(() => { });
		}

		public void Disable()
		{
			Invoke(() => _element.IsEnabled = false);
		}

		public void Capture()
		{
			_dispatcher.Invoke(() =>
			{
			}, DispatcherPriority.Background);
			var screenshot = CaptureScreenshot(_element);
			_snapshotCreator.Add(screenshot, _pose);
		}

		private BitmapSource CaptureScreenshot(FrameworkElement element)
		{
			var pixelWidth = (int) Math.Ceiling(element.ActualWidth);
			var pixelHeight = (int) Math.Ceiling(element.ActualHeight);
			const int dpi = 96;
			return Invoke(() =>
			{
				var image = new RenderTargetBitmap(pixelWidth, pixelHeight, dpi, dpi, PixelFormats.Pbgra32);
				image.Render(element);
				image.Freeze();
				return image;
			});
		}

		private void Invoke(Action action)
		{
			_dispatcher.Invoke(action, DispatcherPriority.Background);
		}

		private TY Invoke<TY>(Func<TY> func)
		{
			var result = default(TY);
			Invoke(() => { result = func(); });
			return result;
		}

		public void Dispose()
		{
		}
	}
}