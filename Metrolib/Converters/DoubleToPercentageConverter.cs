using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts a relative double value to a string in the form of X%.
	///     0 yields 0%
	///     1 yields 100%
	///     0.5 yields 50%
	///     0.55 yields 55%
	///     0.555 yields 56%
	/// </summary>
	/// <remarks>
	///     Does not display any digits after the decimal point.
	/// </remarks>
	public sealed class DoubleToPercentageConverter
		: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double))
				return null;

			var relativeValue = (double) value;
			var formatted = string.Format(culture, "{0:P0}", relativeValue);
			return formatted;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}