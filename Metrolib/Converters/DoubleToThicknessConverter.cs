using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts a <see cref="double" /> value to a <see cref="Thickness" />.
	/// </summary>
	public sealed class DoubleToThicknessConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double))
				return null;

			var v = (double) value;
			return new Thickness(v, v, v, v);
		}

		/// <inheritdoc />
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}