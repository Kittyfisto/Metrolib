using System;
using System.Globalization;
using System.Windows.Data;

// ReSharper disable CheckNamespace

namespace Metrolib.Converters
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A value converter that converts <see cref="TimeSpan" /> values into a written representation that rounds down
	///     to the nearest unit:
	///     - n milisecond(s)
	///     - n second(s)
	///     - n minute(s)
	///     - n hour(s)
	///     - n day(s)
	///     - n week(s)
	///     - n year(s)
	///     - n decade(s)
	/// </summary>
	public sealed class TimeSpanConverter
		: IValueConverter
	{
		/// <summary>
		///     Converts a value.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is TimeSpan))
				return null;

			var age = (TimeSpan) value;
			TimeSpan oneCentury = TimeSpan.FromDays(36525);
			TimeSpan oneDecade = TimeSpan.FromDays(3652.5);
			TimeSpan oneYear = TimeSpan.FromDays(365.25);
			TimeSpan oneMonth = TimeSpan.FromDays(30.5);
			TimeSpan oneWeek = TimeSpan.FromDays(7);
			TimeSpan oneDay = TimeSpan.FromDays(1);
			TimeSpan oneHour = TimeSpan.FromHours(1);
			TimeSpan oneMinute = TimeSpan.FromMinutes(1);
			TimeSpan oneSecond = TimeSpan.FromSeconds(1);
			TimeSpan oneMillisecond = TimeSpan.FromMilliseconds(1);

			if (age >= oneCentury)
				return Format(age, oneCentury, "century", "centuries");
			if (age >= oneDecade)
				return Format(age, oneDecade, "decade");
			if (age >= oneYear)
				return Format(age, oneYear, "year");
			if (age >= oneMonth)
				return Format(age, oneMonth, "month");
			if (age >= oneWeek)
				return Format(age, oneWeek, "week");
			if (age >= oneDay)
				return Format(age, oneDay, "day");
			if (age >= oneHour)
				return Format(age, oneHour, "hour");
			if (age >= oneMinute)
				return Format(age, oneMinute, "minute");
			if (age >= oneSecond)
				return Format(age, oneSecond, "second");

			return Format(age, oneMillisecond, "millisecond");
		}

		/// <summary>
		///     Converts a value.
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

		private object Format(TimeSpan value, TimeSpan divider, string caption)
		{
			return Format(value, divider, caption, caption + "s");
		}

		private object Format(TimeSpan value, TimeSpan divider, string singular, string plural)
		{
			var number = (int) (value.TotalMilliseconds/divider.TotalMilliseconds);
			return string.Format("{0} {1}", number, number == 1 ? singular : plural);
		}
	}
}