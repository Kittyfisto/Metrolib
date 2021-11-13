using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts values of type <see cref="ulong" /> to <see cref="string" /> and vice verca.
	/// </summary>
	public sealed class UInt64ToStringConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is ulong))
				return null;

			var numericValue = (ulong) value;
			var formatted = numericValue.ToString(culture);
			return formatted;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is string))
				return null;

			if (targetType != typeof(ulong) && targetType != typeof(ulong?))
				return null;

			var formatted = (string) value;
			ulong numericValue;
			if (!ulong.TryParse(formatted, NumberStyles.Integer, culture, out numericValue))
				return Binding.DoNothing;

			return numericValue;
		}
	}
}