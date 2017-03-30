using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts null to <see cref="Visibility.Visible" />, anything else to <see cref="Visibility.Collapsed" />.
	/// </summary>
	public sealed class NullToVisibleConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null
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