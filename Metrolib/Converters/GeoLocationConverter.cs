using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Metrolib.Geography;

namespace Metrolib.Converters
{
	/// <summary>
	///     Responsible of converting strings to a <see cref="GeoLocation" />.
	/// </summary>
	public sealed class GeoLocationConverter
		: TypeConverter
	{
		/// <summary>
		/// </summary>
		private static readonly Regex LatitudeLongitudeRegex;

		static GeoLocationConverter()
		{
			LatitudeLongitudeRegex = new Regex(@"^(\d{0,3}\.?\d{0,8})\,\s?(\d{0,3}\.?\d{0,8})$",
			                                   RegexOptions.Compiled |
			                                   RegexOptions.CultureInvariant);
		}

		/// <summary>
		///     Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="sourceType"></param>
		/// <returns></returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof (string))
				return true;

			return CanConvertFrom(sourceType);
		}

		/// <summary>
		///     Returns whether this converter can convert the object to the specified type, using the specified context.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof (string))
			{
				return true;
			}

			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>
		///     Converts the given object to the type of this converter, using the specified context and culture information.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <returns></returns>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var coord = value as string;
			if (coord != null)
			{
				Match match = LatitudeLongitudeRegex.Match(coord);
				if (match.Success)
				{
					double latitude, longitude;
					if (double.TryParse(match.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out latitude) &&
					    double.TryParse(match.Groups[2].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out longitude))
					{
						return new GeoLocation
							{
								Latitude = latitude,
								Longitude = longitude
							};
					}
				}
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>
		///     Converts the given value object to the specified type, using the specified context and culture information.
		/// </summary>
		/// <param name="context"></param>
		/// <param name="culture"></param>
		/// <param name="value"></param>
		/// <param name="destinationType"></param>
		/// <returns></returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
		                                 Type destinationType)
		{
			if (destinationType == typeof (GeoLocation))
			{
				return null;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}