using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Increments the given value by 1.
	/// </summary>
	public sealed class IncrementByOneConverter
		: IValueConverter
	{
		/// <summary>
		///     Increments the given value by 1.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is int))
				return null;

			var actualValue = (int) value;
			return actualValue + 1;
		}

		/// <summary>
		///     Not implemented, returns null.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}