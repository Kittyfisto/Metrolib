using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     To be used when one plans to bind a null value to a <see cref="Image.Source" />.
	///     Without this converter, an error is written to the output log that null is not a valid value for the binding.
	/// </summary>
	public class NullImageConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
				return DependencyProperty.UnsetValue;
			return value;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}