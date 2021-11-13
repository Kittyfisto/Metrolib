using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts values of type <see cref="byte" /> to <see cref="string" /> and vice verca.
	/// </summary>
	public sealed class ByteToStringConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is byte))
				return null;

			var numericValue = (byte)value;
			var formatted = numericValue.ToString(culture);
			return formatted;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is string))
				return null;

			if (targetType != typeof(byte) && targetType != typeof(byte?))
				return null;

			var formatted = (string)value;
			byte numericValue;
			if (!byte.TryParse(formatted, NumberStyles.Integer, culture, out numericValue))
				return Binding.DoNothing;

			return numericValue;
		}
	}
}