using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	/// 
	/// </summary>
	public sealed class TileDownloader
		: ITileCache
	{
		/// <summary>
		/// 
		/// </summary>
		public TileDownloader()
		{
			UriTemplate = "https://tile.openstreetmap.org/{z}/{x}/{y}.png";
		}
		
		/// <summary>
		///     The template under which the tile layer can be queried.
		/// </summary>
		/// <example>
		///     https://tile.openstreetmap.org/{z}/{x}/{y}.png
		/// </example>
		public string UriTemplate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		public async Task<ImageSource> GetTileAsync(Tile tile)
		{
			var uri = CreateUriFromTemplate(tile);
			var request = WebRequest.Create(uri);
			try
			{
				var response = await request.GetResponseAsync();
				var stream = response.GetResponseStream();
				var tmp = new MemoryStream();
				await stream.CopyToAsync(tmp);

				var bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.StreamSource = tmp;
				bitmap.CacheOption = BitmapCacheOption.OnLoad;
				bitmap.CreateOptions = BitmapCreateOptions.None;
				bitmap.EndInit();
				bitmap.Freeze();

				return bitmap;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		[Pure]
		private string CreateUriFromTemplate(Tile tile)
		{
			var tmp = new StringBuilder(UriTemplate);
			tmp.Replace("{x}", tile.X.ToString());
			tmp.Replace("{y}", tile.Y.ToString());
			tmp.Replace("{z}", tile.Z.ToString());
			return tmp.ToString();
		}
	}
}
