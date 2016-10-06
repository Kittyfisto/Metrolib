using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Inverts the given boolean value.
	///     Returns null if no boolean value was given.
	/// </summary>
	public sealed class InvertBoolConverter
		: IValueConverter
	{
		/// <summary>
		///     Inverts the given boolean value.
		///     Returns null if no boolean value was given.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return null;

			return !((bool) value);
		}

		/// <summary>
		///     Inverts the given boolean value.
		///     Returns null if no boolean value was given.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return null;

			return !((bool) value);
		}
	}
}