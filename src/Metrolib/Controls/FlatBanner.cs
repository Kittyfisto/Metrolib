using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Metrolib.Controls
{
	/// <summary>
	///     A dialog like banner which blocks the main window until hidden.
	/// </summary>
	public class FlatBanner
		: ContentControl
	{
		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty OverlayBrushProperty = DependencyProperty.Register(
		                                                                                             "OverlayBrush",
		                                                                                             typeof(Brush),
		                                                                                             typeof(FlatBanner),
		                                                                                             new
			                                                                                             PropertyMetadata(default
			                                                                                                              (
				                                                                                                              Brush
			                                                                                                              )));

		private Window _window;

		static FlatBanner()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(FlatBanner),
			                                         new FrameworkPropertyMetadata(typeof(FlatBanner)));
		}

		/// <summary>
		/// </summary>
		public FlatBanner()
		{
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
		}

		/// <summary>
		/// 
		/// </summary>
		public Brush OverlayBrush
		{
			get => (Brush) GetValue(OverlayBrushProperty);
			set => SetValue(OverlayBrushProperty, value);
		}

		private void OnLoaded(object sender, RoutedEventArgs e)
		{
			_window = this.FirstAncestorOfTypeOrDefault<Window>();
			if (_window != null) _window.SizeChanged += WindowOnSizeChanged;
		}

		private void WindowOnSizeChanged(object sender, SizeChangedEventArgs e)
		{
			
		}

		private void OnUnloaded(object sender, RoutedEventArgs e)
		{
			if (_window != null)
			{
				_window.SizeChanged -= WindowOnSizeChanged;
				_window = null;
			}
		}
	}
}