using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	public sealed class UInt16ToStringConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is ushort))
				return null;

			var numericValue = (ushort)value;
			var formatted = numericValue.ToString(culture);
			return formatted;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is string))
				return null;

			if (targetType != typeof(ushort))
				return null;

			var formatted = (string)value;
			ushort numericValue;
			if (!ushort.TryParse(formatted, NumberStyles.Integer, culture, out numericValue))
				return Binding.DoNothing;

			return numericValue;
		}
	}
}