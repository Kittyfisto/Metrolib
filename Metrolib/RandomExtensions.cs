using System;
using System.Windows;

namespace Metrolib
{
	/// <summary>
	///     Collects extension methods to the <see cref="Random" /> class.
	/// </summary>
	public static class RandomExtensions
	{
		/// <summary>
		///     Returns a random direction as a unit vector.
		/// </summary>
		/// <param name="rng"></param>
		/// <returns></returns>
		public static Vector NextDirection(this Random rng)
		{
			var dir = new Vector(2*rng.NextDouble() - 1, 2*rng.NextDouble() - 1);
			dir.Normalize();
			return dir;
		}
	}
}