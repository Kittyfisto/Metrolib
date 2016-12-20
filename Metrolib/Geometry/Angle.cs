using System;
using System.Diagnostics.Contracts;

namespace Metrolib.Geometry
{
	/// <summary>
	///     Helper class to help deal with angle from/to radian conversions.
	/// </summary>
	public struct Angle
	{
		/// <summary>
		///     Converts an angle in degrees to radians.
		/// </summary>
		/// <param name="degrees"></param>
		/// <returns></returns>
		[Pure]
		public static double ToRadians(double degrees)
		{
			return degrees/180*Math.PI;
		}

		/// <summary>
		///     Converts an angle in radians to degrees.
		/// </summary>
		/// <param name="radians"></param>
		/// <returns></returns>
		[Pure]
		public static double ToDegrees(double radians)
		{
			return radians/Math.PI*180;
		}
	}
}