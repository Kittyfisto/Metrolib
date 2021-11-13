using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
		struct TimeUnit
		{
			private readonly string _singular;
			private readonly string _plural;
			public readonly TimeSpan Duration;
			public readonly Unit Unit;

			public TimeUnit(Unit unit, TimeSpan duration, string singular)
			: this(unit, duration, singular, singular+"s")
			{}

			public TimeUnit(Unit unit, TimeSpan duration, string singular, string plural)
			{
				Unit = unit;
				Duration = duration;
				_singular = singular;
				_plural = plural;
			}

			public string Format(TimeSpan value)
			{
				int number;
				if (value < Duration)
					number = 1;
				else
					number = (int) (value.TotalMilliseconds/Duration.TotalMilliseconds);

				return string.Format("{0} {1}", number, number == 1 ? _singular : _plural);
			}
		}

		private readonly IReadOnlyList<TimeUnit> _allUnits;
		private IReadOnlyList<TimeUnit> _usedUnits;

		/// <summary>
		/// 
		/// </summary>
		public TimeSpanConverter()
		{
			_allUnits = new[]
			{
				new TimeUnit(Unit.Century, TimeSpan.FromDays(36525), "century", "centuries"),
				new TimeUnit(Unit.Decade, TimeSpan.FromDays(3652.5), "decade"),
				new TimeUnit(Unit.Year, TimeSpan.FromDays(365.25), "year"),
				new TimeUnit(Unit.Month, TimeSpan.FromDays(30.5), "month"),
				new TimeUnit(Unit.Week, TimeSpan.FromDays(7), "week"),
				new TimeUnit(Unit.Day, TimeSpan.FromDays(1), "day"),
				new TimeUnit(Unit.Hour, TimeSpan.FromHours(1), "hour"),
				new TimeUnit(Unit.Minute, TimeSpan.FromMinutes(1), "minute"),
				new TimeUnit(Unit.Second, TimeSpan.FromSeconds(1), "second"),
				new TimeUnit(Unit.Millisecond, TimeSpan.FromMilliseconds(1), "millisecond")
			};
			_usedUnits = _allUnits;
		}

		public IReadOnlyList<Unit> UsedUnits
		{
			get { return _usedUnits?.Select(x => x.Unit).ToList();}
			set
			{
				_usedUnits = value != null
					? _allUnits.Where(x => value.Contains(x.Unit)).ToList()
					: null;
			}
		}

		public IReadOnlyList<Unit> IgnoredUnits
		{
			get
			{
				var used = UsedUnits;
				return used != null
					? _allUnits.Select(x => x.Unit).Except(UsedUnits).ToList()
					: _allUnits.Select(x => x.Unit).ToList();
			}
			set
			{
				UsedUnits = _allUnits.Select(x => x.Unit).Except(value).ToList();
			}
		}

		/// <inheritdoc />
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is TimeSpan))
				return null;

			var age = (TimeSpan) value;
			if (_usedUnits.Count == 0)
				return null;

			for(int i = 0; i < _usedUnits.Count -1; ++i)
			{
				var unit = _usedUnits[i];
				if (age >= unit.Duration)
					return unit.Format(age);
			}

			return _usedUnits[_usedUnits.Count - 1].Format(age);
		}

		/// <inheritdoc />
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}