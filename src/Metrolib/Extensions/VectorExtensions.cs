using System.Windows;

// ReSharper disable once CheckNamespace
namespace Metrolib
{
	/// <summary>
	/// </summary>
	public static class VectorExtensions
	{
		/// <summary>
		///     Returns a vector that is clamped to the given length.
		/// </summary>
		/// <param name="that"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static Vector Clamped(this Vector that, double maxLength)
		{
			double length = that.Length;
			double div = length/maxLength;
			if (div > 1)
			{
				return that/div;
			}
			return that;
		}

		/// <summary>
		///     Returns a normalized vector of this.
		/// </summary>
		/// <param name="that"></param>
		/// <returns></returns>
		public static Vector Normalized(this Vector that)
		{
			that.Normalize();
			return that;
		}
	}
}