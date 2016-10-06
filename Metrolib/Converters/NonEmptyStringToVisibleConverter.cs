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
		/// <summary>
		///     Converts non-empty strings to <see cref="Visibility.Visible" /> and everything
		///     else to <see cref="Visibility.Collapsed" />.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var actualValue = value as string;
			if (actualValue == null)
				return Visibility.Collapsed;

			return actualValue.Length > 0
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