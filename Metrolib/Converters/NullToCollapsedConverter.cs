using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts null to <see cref="Visibility.Visible" />, anything else to <see cref="Visibility.Visible" />.
	/// </summary>
	public sealed class NullToCollapsedConverter
		: IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null
				? Visibility.Collapsed
				: Visibility.Visible;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}