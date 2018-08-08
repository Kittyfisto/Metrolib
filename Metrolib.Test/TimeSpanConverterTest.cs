using System;
using System.Globalization;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class TimeSpanConverterTest
	{
		[SetUp]
		public void Setup()
		{
			_converter = new TimeSpanConverter();
		}

		private TimeSpanConverter _converter;

		[Test]
		public void TestSetOneIgnoredUnit()
		{
			_converter.IgnoredUnits = new[] {Unit.Millisecond};
			_converter.Convert(TimeSpan.FromMilliseconds(10), typeof(string), null, CultureInfo.CurrentUICulture)
			          .Should().Be("1 second");
		}

		[Test]
		public void TestConvert1()
		{
			_converter.Convert(TimeSpan.FromTicks(1000), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 millisecond");
			_converter.Convert(TimeSpan.FromMilliseconds(1), typeof (string), null, CultureInfo.CurrentUICulture).Should().Be("1 millisecond");
			_converter.Convert(TimeSpan.FromMilliseconds(2), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 milliseconds");
			_converter.Convert(TimeSpan.FromMilliseconds(999), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("999 milliseconds");
		}

		[Test]
		public void TestConvert2()
		{
			_converter.Convert(TimeSpan.FromSeconds(1), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 second");
			_converter.Convert(TimeSpan.FromSeconds(2), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 seconds");
			_converter.Convert(TimeSpan.FromSeconds(59), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("59 seconds");
		}

		[Test]
		public void TestConvert3()
		{
			_converter.Convert(TimeSpan.FromMinutes(1), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 minute");
			_converter.Convert(TimeSpan.FromMinutes(2), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 minutes");
			_converter.Convert(TimeSpan.FromMinutes(59), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("59 minutes");
		}

		[Test]
		public void TestConvert4()
		{
			_converter.Convert(TimeSpan.FromHours(1), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 hour");
			_converter.Convert(TimeSpan.FromHours(2), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 hours");
			_converter.Convert(TimeSpan.FromHours(23), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("23 hours");
		}

		[Test]
		public void TestConvert5()
		{
			_converter.Convert(TimeSpan.FromDays(1), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 day");
			_converter.Convert(TimeSpan.FromDays(2), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 days");
			_converter.Convert(TimeSpan.FromDays(6), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("6 days");
		}

		[Test]
		public void TestConvert6()
		{
			_converter.Convert(TimeSpan.FromDays(7), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 week");
			_converter.Convert(TimeSpan.FromDays(14), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 weeks");
			_converter.Convert(TimeSpan.FromDays(29), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("4 weeks");
		}

		[Test]
		public void TestConvert7()
		{
			_converter.Convert(TimeSpan.FromDays(30.5), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 month");
			_converter.Convert(TimeSpan.FromDays(61), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 months");
			_converter.Convert(TimeSpan.FromDays(364), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("11 months");
		}

		[Test]
		public void TestConvert8()
		{
			_converter.Convert(TimeSpan.FromDays(365.25), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 year");
			_converter.Convert(TimeSpan.FromDays(800), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 years");
			_converter.Convert(TimeSpan.FromDays(3649), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("9 years");
		}

		[Test]
		public void TestConvert9()
		{
			_converter.Convert(TimeSpan.FromDays(3652.5), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 decade");
			_converter.Convert(TimeSpan.FromDays(7305), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 decades");
			_converter.Convert(TimeSpan.FromDays(36524), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("9 decades");
		}

		[Test]
		public void TestConvert10()
		{
			_converter.Convert(TimeSpan.FromDays(36525), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("1 century");
			_converter.Convert(TimeSpan.FromDays(73050), typeof(string), null, CultureInfo.CurrentUICulture).Should().Be("2 centuries");
		}

		[Test]
		public void TestConvert11()
		{
			new Action(() => _converter.Convert(null, typeof(string), null, CultureInfo.InvariantCulture)).ShouldNotThrow();
			new Action(() => _converter.Convert(42, typeof(string), null, CultureInfo.InvariantCulture)).ShouldNotThrow();
			new Action(() => _converter.Convert("foobar", typeof(string), null, CultureInfo.InvariantCulture)).ShouldNotThrow();
		}
	}
}