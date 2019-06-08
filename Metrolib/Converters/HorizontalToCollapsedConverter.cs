using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts <see cref="Orientation.Horizontal" /> to <see cref="Visibility.Collapsed" />,
	///     everything else to <see cref="Visibility.Visible" />.
	/// </summary>
	public sealed class HorizontalToCollapsedConverter
		: IValueConverter
	{
		#region Implementation of IValueConverter

		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is Orientation))
				return Visibility.Visible;

			var orientation = (Orientation) value;
			if (orientation == Orientation.Horizontal)
				return Visibility.Collapsed;

			return Visibility.Visible;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}