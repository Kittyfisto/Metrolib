using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts <see cref="Color" /> values to <see cref="SolidColorBrush" />.
	/// </summary>
	public sealed class ColorToBrushConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Color))
				return null;

			var color = (Color) value;
			var brush = new SolidColorBrush(color);
			brush.Freeze();
			return brush;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}