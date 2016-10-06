using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     This converter converts an empty or null string to Visible, anything else to Collapsed.
	/// </summary>
	public class NullOrEmptyStringToVisibleConverter
		: IValueConverter
	{
		/// <summary>
		///     Converts an empty or null string to Visible, anything else to Collapsed.
		/// </summary>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var enumeration = value as string;
			if (enumeration == null)
				return Visibility.Visible;

			return enumeration.Length == 0
				       ? Visibility.Visible
				       : Visibility.Collapsed;
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