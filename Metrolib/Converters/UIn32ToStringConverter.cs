using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts values of type <see cref="uint" /> to <see cref="string" /> and vice verca.
	/// </summary>
	public sealed class UInt32ToStringConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is uint))
				return null;

			var numericValue = (uint) value;
			var formatted = numericValue.ToString(culture);
			return formatted;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is string))
				return null;

			if (targetType != typeof(uint) && targetType != typeof(uint?))
				return null;

			var formatted = (string) value;
			uint numericValue;
			if (!uint.TryParse(formatted, NumberStyles.Integer, culture, out numericValue))
				return Binding.DoNothing;

			return numericValue;
		}
	}
}