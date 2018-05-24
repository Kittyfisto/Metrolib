using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	/// </summary>
	public class TileControl
		: FrameworkElement
	{
		/// <summary>
		/// </summary>
		public static readonly DependencyProperty PositionProperty = DependencyProperty.Register(
		                                                                                         "Position", typeof(Tile),
		                                                                                         typeof(TileControl),
		                                                                                         new PropertyMetadata(default(
			                                                                                                              Tile
		                                                                                                              )));

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty ImageTaskProperty = DependencyProperty.Register(
		                                                                                          "ImageTask",
		                                                                                          typeof(Task<ImageSource>),
		                                                                                          typeof(TileControl),
		                                                                                          new
			                                                                                          PropertyMetadata(defaultValue: null,
			                                                                                                           propertyChangedCallback
			                                                                                                           : OnImageChanged));

		/// <summary>
		/// </summary>
		public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
		                                                                                      "Image", typeof(ImageSource),
		                                                                                      typeof(TileControl),
		                                                                                      new PropertyMetadata(default(
			                                                                                                           ImageSource
		                                                                                                           )));

		/// <summary>
		/// </summary>
		public Tile Position
		{
			get => (Tile) GetValue(PositionProperty);
			set => SetValue(PositionProperty, value);
		}

		/// <summary>
		/// </summary>
		public ImageSource Image
		{
			get => (ImageSource) GetValue(ImageProperty);
			set => SetValue(ImageProperty, value);
		}

		/// <summary>
		/// </summary>
		public Task<ImageSource> ImageTask
		{
			get => (Task<ImageSource>) GetValue(ImageTaskProperty);
			set => SetValue(ImageTaskProperty, value);
		}

		private static void OnImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue is Task<ImageSource> task) ((TileControl) d).FetchTileAsync(task);
		}

		private async void FetchTileAsync(Task<ImageSource> imageTask)
		{
			try
			{
				var image = await imageTask;
				await SetImage(image);
			}
			catch (Exception e)
			{
				// TODO: Log exception once this has been moved to a different assembly (and log4net is referenced there)
				await SetImage(null);
			}
		}

		private DispatcherOperation SetImage(ImageSource image)
		{
			return Dispatcher.BeginInvoke(new Action(() =>
			{
				Image = image;
			}));
		}
	}
}