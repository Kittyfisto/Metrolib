using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Converts any <see cref="IEnumerable" /> that contains at least one value
	///     to <see cref="Visibility.Collapsed" />. If it is empty, then <see cref="Visibility.Visible" />
	///     is returned. Anything else yields null.
	/// </summary>
	public sealed class NonEmptyToCollapsedConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var actualValue = value as IEnumerable;
			if (actualValue == null)
				return null;

			var it = actualValue.GetEnumerator();
			return it.MoveNext()
				? Visibility.Collapsed
				: Visibility.Visible;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}