using System.ComponentModel;
using Metrolib.Converters;

namespace Metrolib.Geography
{
	/// <summary>
	///     Represents a point on earth in WGS84 space.
	/// </summary>
	[TypeConverter(typeof (GeoLocationConverter))]
	public struct GeoLocation
	{
		/// <summary>
		///     Where the equator and null meridian meet.
		/// </summary>
		public static readonly GeoLocation Zero;

		private double _latitude;
		private double _longitude;

		static GeoLocation()
		{
			Zero = new GeoLocation(0, 0);
		}

		private GeoLocation(double latitude, double longitude)
		{
			_latitude = latitude;
			_longitude = longitude;
		}

		/// <summary>
		///     Latitude value of this location.
		/// </summary>
		public double Latitude
		{
			get { return _latitude; }
			set { _latitude = value; }
		}

		/// <summary>
		///     Longitude value of this location.
		/// </summary>
		public double Longitude
		{
			get { return _longitude; }
			set { _longitude = value; }
		}

		/// <summary>
		///     Returns a string that represents the current object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("{0}°N, {1}°E", Latitude, Longitude);
		}
	}
}