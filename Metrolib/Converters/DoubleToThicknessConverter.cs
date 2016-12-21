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
		/// <summary>
		///     Converts a <see cref="double" /> value to a <see cref="Thickness" />.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double))
				return null;

			var v = (double) value;
			return new Thickness(v, v, v, v);
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}