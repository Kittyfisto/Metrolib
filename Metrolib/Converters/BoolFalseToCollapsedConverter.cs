using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

// ReSharper disable CheckNamespace

namespace Metrolib.Converters
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Converts false to <see cref="Visibility.Collapsed" />, true to <see cref="Visibility.Visible" />
	///     and anything else to null.
	/// </summary>
	public sealed class BoolFalseToCollapsedConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof (Visibility))
				return null;

			if (!(value is bool))
				return null;

			var val = (bool) value;
			if (!val)
			{
				return Visibility.Collapsed;
			}

			return Visibility.Visible;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}