using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     A value converter that converts non-empty strings to <see cref="Visibility.Visible" /> and everything
	///     else to <see cref="Visibility.Collapsed" />.
	/// </summary>
	public sealed class NonEmptyStringToVisibleConverter
		: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var actualValue = value as string;
			if (actualValue == null)
				return Visibility.Collapsed;

			return actualValue.Length > 0
				       ? Visibility.Visible
				       : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}