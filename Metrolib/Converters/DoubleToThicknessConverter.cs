using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts a double value to a thickness where all four components are set to the given value.
	/// </summary>
	public sealed class DoubleToThicknessConverter
		: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double))
				return null;

			var v = (double) value;
			return new Thickness(v, v, v, v);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}