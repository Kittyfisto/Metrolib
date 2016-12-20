using System.Collections.Generic;
using System.Windows.Controls;

// ReSharper disable CheckNamespace

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     The canvas that actually presents all layers of a <see cref="MapView" />.
	/// </summary>
	public class MapCanvas
		: Grid
	{
		/// <summary>
		///     Adds the given layers to this canvas.
		/// </summary>
		/// <param name="newStartingIndex"></param>
		/// <param name="newItems"></param>
		public void AddLayers(int newStartingIndex, IEnumerable<Layer> newItems)
		{
			foreach (Layer layer in newItems)
			{
				Children.Add(layer);
			}
		}
	}
}