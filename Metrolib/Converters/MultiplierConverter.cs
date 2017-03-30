using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     Multiplies the given numeric value with the given <see cref="Multiplier" />.
	/// </summary>
	public sealed class MultiplierConverter
		: IValueConverter
	{
		/// <summary>
		///     The value used to multiply any value given to <see cref="Convert" />.
		/// </summary>
		public double Multiplier { get; set; }

		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double))
				return null;

			var actualValue = (double) value;
			return actualValue*Multiplier;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}