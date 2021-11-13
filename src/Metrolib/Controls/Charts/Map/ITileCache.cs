using System.Threading.Tasks;
using System.Windows.Media;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     Responsible for retrieving the actual images for a specific tile.
	/// </summary>
	public interface ITileCache
	{
		/// <summary>
		/// </summary>
		/// <param name="tile"></param>
		/// <returns></returns>
		Task<ImageSource> GetTileAsync(Tile tile);
	}
}