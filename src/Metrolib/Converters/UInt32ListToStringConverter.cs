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
	public sealed class UInt32ListToStringConverter
		: AbstractNumericListToStringConverter<UInt32>
	{
		/// <inheritdoc />
		protected override bool AreAdjacent(uint lhs, uint rhs)
		{
			var min = Math.Min(lhs, rhs);
			var max = Math.Max(lhs, rhs);
			var dist = max - min;
			return dist == 1;
		}

		/// <inheritdoc />
		protected override IEnumerable<uint> GetRange(uint firstValue, uint lastValue)
		{
			var min = Math.Min(firstValue, lastValue);
			var max = Math.Max(firstValue, lastValue);
			var count = max - min + 1;
			var values = new List<UInt32>((int) count);
			for (int i = 0; i < count; ++i)
			{
				values.Add((uint) (min + i));
			}
			return values;
		}

		/// <inheritdoc />
		protected override bool TryParse(string part, out uint value, CultureInfo culture)
		{
			return UInt32.TryParse(part, NumberStyles.Integer, culture, out value);
		}
	}
}