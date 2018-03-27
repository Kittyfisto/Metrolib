using System;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
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
				SetKeyboardFocused();
				_element.Focus();

				MakeFocusBorderVisible();
			});
		}

		private void MakeFocusBorderVisible()
		{
			// I'm too fucking stupid to get the fucking animation to work in this application
			// and therefore I have to cheat...
			var method = typeof(T).GetMethod("GetTemplateChild", BindingFlags.NonPublic | BindingFlags.Instance);
			var focusBorder = (Border)method.Invoke(_element, new object[] {"focusBorder"});
			focusBorder.Opacity = 1;
		}

		private void SetKeyboardFocused()
		{
			// Black magic necessary to make the control to believe that it's keyboard focused.
			// Haven't found an easier wa to make this work (and no, Keyboard.Focus() has no fucking effect).

			var field = typeof(UIElement).GetField("_flags", BindingFlags.NonPublic | BindingFlags.Instance);
			var flags = Convert.ToInt32(field.GetValue(_element));
			flags |= 0x00000400;
			flags |= 0x00000800;
			var type = typeof(UIElement).Assembly.GetType("System.Windows.CoreFlags");
			var newFlags = Enum.ToObject(type, flags);
			field.SetValue(_element, newFlags);

			var method = typeof(UIElement).GetMethod("RaiseIsKeyboardFocusWithinChanged", BindingFlags.NonPublic | BindingFlags.Instance);
			method.Invoke(_element, new object[]
			{
				new DependencyPropertyChangedEventArgs(UIElement.IsKeyboardFocusWithinProperty, false, true)
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
				var screenshot = CaptureScreenshot(_element);
				_snapshotCreator.Add(screenshot, _pose);
			}, DispatcherPriority.Background);
		}

		public void Dispose()
		{}

		private BitmapSource CaptureScreenshot(FrameworkElement element)
		{
			var pixelWidth = (int) Math.Ceiling(element.ActualWidth);
			var pixelHeight = (int) Math.Ceiling(element.ActualHeight);
			const int dpi = 96;
			var image = new RenderTargetBitmap(pixelWidth, pixelHeight, dpi, dpi, PixelFormats.Pbgra32);
			image.Render(element);
			image.Freeze();
			return image;
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
	}
}