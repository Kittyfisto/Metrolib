// ReSharper disable CheckNamespace

using System.Collections.Generic;

namespace Metrolib
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// 
	/// </summary>
	public interface IPieSeries
	{
		/// <summary>
		/// 
		/// </summary>
		IEnumerable<IPieSlice> Slices { get; }
	}
}