using System.Windows;

namespace Metrolib
{
	/// <summary>
	/// 
	/// </summary>
	public static class VectorExtensions
	{
		/// <summary>
		/// 
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
