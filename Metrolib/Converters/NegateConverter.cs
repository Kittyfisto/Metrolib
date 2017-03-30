using System;
using System.Globalization;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	///     A converter that negates any given signed number.
	///     Supports <see cref="decimal" />, <see cref="double" />, <see cref="float" />,
	///     <see cref="long" />, <see cref="int" />, <see cref="short" /> and <see cref="sbyte" />.
	/// </summary>
	public class NegateConverter
		: IValueConverter
	{
		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType == typeof (Decimal))
			{
				decimal actualValue = System.Convert.ToDecimal(value);
				return -actualValue;
			}

			if (targetType == typeof (double))
			{
				double actualValue = System.Convert.ToDouble(value);
				return -actualValue;
			}

			if (targetType == typeof (float))
			{
				float actualValue = System.Convert.ToSingle(value);
				return -actualValue;
			}

			if (targetType == typeof (long))
			{
				long actualValue = System.Convert.ToInt64(value);
				return -actualValue;
			}

			if (targetType == typeof (int))
			{
				int actualValue = System.Convert.ToInt32(value);
				return -actualValue;
			}

			if (targetType == typeof (short))
			{
				short actualValue = System.Convert.ToInt16(value);
				return -actualValue;
			}

			if (targetType == typeof (sbyte))
			{
				sbyte actualValue = System.Convert.ToSByte(value);
				return -actualValue;
			}

			return null;
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert(value, targetType, parameter, culture);
		}
	}
}