using System;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Windows.Data;

// ReSharper disable CheckNamespace

namespace Metrolib.Converters
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     Responsible for converting (big) numbers to readable text, omitting uninteresting decimals, e.g.
	///     12.344.112 becomes 12M.
	/// </summary>
	public sealed class CountConverter
		: IValueConverter
	{
		private const int Million = 1000000;
		private const int Thousand = 1000;

		/// <summary>
		///     Initializes this converter.
		/// </summary>
		public CountConverter()
		{
			HasPlural = true;
		}

		/// <summary>
		///     The suffic that should be added to the final string.
		/// </summary>
		public string Suffix { get; set; }

		/// <summary>
		///     Whether or not a plural 's' should be added to the suffix when the remaining count
		///     is not 1.
		/// </summary>
		public bool HasPlural { get; set; }

		/// <summary>
		///     Converts the given numeric value to readable text, omitting uninteresting decimals, e.g.
		///     12.344.112 becomes 12M.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="targetType"></param>
		/// <param name="parameter"></param>
		/// <param name="culture"></param>
		/// <returns></returns>
		[Pure]
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is int))
				return "-";

			var count = (int) value;
			if (count > Million)
				return Format(culture, count/Million, count, "M");
			if (count > Thousand)
				return Format(culture, count/Thousand, count, "k");

			return Format(culture, count, count, "");
		}

		/// <summary>
		///     No implemented, returns null.
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

		[Pure]
		private string Format(CultureInfo culture, int count, int totalCount, string quantifier)
		{
			if (!string.IsNullOrEmpty(Suffix))
			{
				if (HasPlural && totalCount != 1)
				{
					return string.Format(culture, "{0}{1} {2}s", count, quantifier, Suffix);
				}

				return string.Format(culture, "{0}{1} {2}", count, quantifier, Suffix);
			}

			return string.Format(culture, "{0}{1}", count, quantifier);
		}
	}
}