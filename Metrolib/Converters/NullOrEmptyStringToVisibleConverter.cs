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
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var enumeration = value as string;
			if (enumeration == null)
				return Visibility.Visible;

			return enumeration.Length == 0
				       ? Visibility.Visible
				       : Visibility.Collapsed;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}