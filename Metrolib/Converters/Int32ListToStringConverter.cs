using System;
using System.Collections.Generic;
using System.Globalization;

namespace Metrolib.Converters
{
	/// <summary>
	/// Responsible for converting a list of numbers into a string representation and back.
	/// </summary>
	/// <example>
	/// Convert({}) returns ""
	/// Convert({1}) returns "1"
	/// Convert({1, 3}) returns "1, 3"
	/// Convert({1, 2, 3}) returns "1-3"
	/// Convert({1, 4, 5, 6, 7}) returns "1, 4-7"
	/// </example>
	public sealed class Int32ListToStringConverter
		: AbstractNumericListToStringConverter<int>
	{
		/// <inheritdoc />
		protected override bool AreAdjacent(int lhs, int rhs)
		{
			return Math.Abs(lhs - rhs) == 1;
		}

		/// <inheritdoc />
		protected override IEnumerable<int> GetRange(int firstValue, int lastValue)
		{
			var min = Math.Min(firstValue, lastValue);
			var max = Math.Max(firstValue, lastValue);
			var count = max - min;
			var values = new List<int>(count);
			for (int i = 0; i < count; ++i)
			{
				values[i] = min + i;
			}
			return values;
		}

		/// <inheritdoc />
		protected override bool TryParse(string part, out int value, CultureInfo culture)
		{
			return int.TryParse(part, NumberStyles.Integer, culture, out value);
		}
	}
}