using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Metrolib.Converters
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class AbstractNumericListToStringConverter<T>
		: IValueConverter
		where T : IComparable<T>, IEquatable<T>
	{
		protected AbstractNumericListToStringConverter()
		{
			Separator = ", ";
			RangeConnector = "-";
		}

		/// <summary>
		/// The separator used to separate multiple values.
		/// Set to ", " by default.
		/// </summary>
		/// <example>
		/// 1, 2, 3
		/// </example>
		public string Separator { get; set; }

		/// <summary>
		/// The string used to denote a range between of values between two ends.
		/// Set to "-" by default.
		/// </summary>
		/// <example>
		/// 1-3
		/// </example>
		public string RangeConnector { get; set; }

		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var inputValues = value as IEnumerable<T>;
			if (inputValues == null)
				return null;

			using (var ordered = inputValues.OrderBy(x => x).GetEnumerator())
			{
				if (ordered.MoveNext())
				{
					var last = new Values(ordered.Current);
					var values = new List<Values> { last };
					while (ordered.MoveNext())
					{
						var current = ordered.Current;
						if (AreAdjacent(last.Last, current))
						{
							last.Last = current;
							values[values.Count - 1] = last;
						}
						else
						{
							last = new Values(current);
							values.Add(last);
						}
					}

					return Output(values, culture);
				}

				return "";
			}
		}

		private string Output(List<Values> values, CultureInfo culture)
		{
			var builder = new StringBuilder();
			for(int i = 0; i < values.Count; ++i)
			{
				if (i != 0)
					builder.Append(Separator);

				var value = values[i];
				if (!value.Last.Equals(value.First))
				{
					builder.AppendFormat(culture, "{0}{1}{2}", value.First, RangeConnector, value.Last);
				}
				else
				{
					builder.AppendFormat(culture, "{0}", value.First);
				}
			}
			return builder.ToString();
		}
		
		/// <summary>
		/// Tests whether the two given values are adjacent to each other.
		/// If set to true, then they will be written as "lhs - rhs" instead
		/// of "lhs, rhs".
		/// </summary>
		/// <param name="lhs"></param>
		/// <param name="rhs"></param>
		/// <returns></returns>
		protected abstract bool AreAdjacent(T lhs, T rhs);

		struct Values
		{
			public readonly T First;
			public T Last;

			public Values(T value)
			{
				First = value;
				Last = value;
			}
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType == null)
				return null;

			var str = value as string;
			var values = ParseValues(str, culture);
			if (values == null)
				return Binding.DoNothing;

			return CreateTargetType(targetType, values);
		}

		private IEnumerable<T> ParseValues(string str, CultureInfo culture)
		{
			str = str ?? "";
			var values = new List<T>();
			int current = 0;
			int previous = 0;
			while((current = str.IndexOf(Separator, current)) != -1)
			{
				if (!ParsePart(str, previous, current-previous, culture, values))
					return null;

				++current;
				previous = current;
			}

			if (previous < str.Length)
				if (!ParsePart(str, previous, str.Length - previous, culture, values))
					return null;

			return values;
		}

		private bool ParsePart(string text, int start, int count, CultureInfo culture, List<T> values)
		{
			var part = text.Substring(start, count).Trim();
			int n = part.IndexOf(RangeConnector, StringComparison.CurrentCulture);
			if (n == -1)
			{
				T value;
				if (!TryParse(part, out value, culture))
					return false;

				values.Add(value);
			}
			else
			{
				var first = part.Substring(0, n);
				var last = part.Substring(n + 1);
				T firstValue, lastValue;
				if (!TryParse(first, out firstValue, culture))
					return false;
				if (!TryParse(last, out lastValue, culture))
					return false;

				values.AddRange(GetRange(firstValue, lastValue));
			}

			return true;
		}

		[Pure]
		protected abstract IEnumerable<T> GetRange(T firstValue, T lastValue);
		protected abstract bool TryParse(string part, out T value, CultureInfo culture);

		private object CreateTargetType(Type targetType, IEnumerable<T> values)
		{
			if (typeof(IEnumerable<T>) == targetType)
			{
				return values.ToList();
			}
			if (typeof(T[]) == targetType)
			{
				return values.ToArray();
			}
			if (typeof(List<T>) == targetType)
			{
				return values.ToList();
			}

			return Activator.CreateInstance(targetType, values);
		}
	}
}