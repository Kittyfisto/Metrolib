using System;
using System.Globalization;
using System.Xml;

// ReSharper disable once CheckNamespace
namespace Metrolib
{
	public static class XmlExtensions
	{
		public static T ReadContentAsEnum<T>(this XmlReader reader)
		{
			string value = reader.Value;
			return (T)Enum.Parse(typeof(T), value);
		}

		public static Guid ReadContentAsGuid(this XmlReader reader)
		{
			string value = reader.Value;
			return Guid.Parse(value);
		}

		public static DateTime ReadContentAsDateTime2(this XmlReader reader)
		{
			string stringValue = reader.Value;
			long value = long.Parse(stringValue, NumberStyles.Integer, CultureInfo.InvariantCulture);
			var dateTime = new DateTime(value);
			return dateTime;
		}

		public static double ReadContentAsDouble2(this XmlReader reader)
		{
			string stringValue = reader.Value;
			double value = double.Parse(stringValue, NumberStyles.Float, CultureInfo.InvariantCulture);
			return value;
		}

		public static bool ReadContentAsBool(this XmlReader reader)
		{
			string value = reader.Value;
			return string.Equals(value, "true", StringComparison.InvariantCultureIgnoreCase);
		}

		public static byte[] ReadContentAsBase64(this XmlReader reader)
		{
			string stringValue = reader.Value;
			var value = Convert.FromBase64String(stringValue);
			return value;
		}

		public static void WriteAttributeDateTime(this XmlWriter writer, string localName, DateTime value)
		{
			string stringValue = value.Ticks.ToString(CultureInfo.InvariantCulture);
			writer.WriteAttributeString(localName, stringValue);
		}

		public static void WriteAttributeDouble(this XmlWriter writer, string localName, double value)
		{
			string stringValue = value.ToString(CultureInfo.InvariantCulture);
			writer.WriteAttributeString(localName, stringValue);
		}

		/// <summary>
		/// Writes an attribute with the given guid as string.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="localName"></param>
		/// <param name="value"></param>
		public static void WriteAttributeGuid(this XmlWriter writer, string localName, Guid value)
		{
			string stringValue = value.ToString();
			writer.WriteAttributeString(localName, stringValue);
		}

		/// <summary>
		/// Writes an attribute with the given boolean value as string (either true or false).
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="localName"></param>
		/// <param name="value"></param>
		public static void WriteAttributeBool(this XmlWriter writer, string localName, bool value)
		{
			writer.WriteAttributeString(localName, value ? "true" : "false");
		}

		/// <summary>
		/// Writes an attribute with the given integer value in base 10.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="localname"></param>
		/// <param name="value"></param>
		public static void WriteAttributeInt(this XmlWriter writer, string localname, int value)
		{
			writer.WriteAttributeString(localname, value.ToString(CultureInfo.InvariantCulture));
		}

		/// <summary>
		/// Writes an attribute with the name of the given enum value.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="writer"></param>
		/// <param name="localName"></param>
		/// <param name="value"></param>
		public static void WriteAttributeEnum<T>(this XmlWriter writer, string localName, T value)
		{
			writer.WriteAttributeString(localName, value.ToString());
		}

		/// <summary>
		/// Writes an attribute with the given byte array (base 64 decoded).
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="localName"></param>
		/// <param name="value"></param>
		public static void WriteAttributeBase64(this XmlWriter writer, string localName, byte[] value)
		{
			var stringValue = value != null
								  ? Convert.ToBase64String(value)
								  : string.Empty;

			writer.WriteAttributeString(localName, stringValue);
		}
	}
}
