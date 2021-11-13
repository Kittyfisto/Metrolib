using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class Int32ListToStringConverterTest
		: AbstractListToStringConverterTest<Int32>
	{
		private Int32ListToStringConverter _converter;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_converter = new Int32ListToStringConverter();
		}

		[Test]
		public void TestConvert1()
		{
			_converter.Convert(new Int32[] { }, null, null, null).Should().Be("");
		}

		[Test]
		public void TestConvert2()
		{
			_converter.Convert(new Int32[] { 1 }, null, null, null).Should().Be("1");
		}

		[Test]
		public void TestConvert3()
		{
			_converter.Convert(new Int32[] { 1, 3 }, null, null, null).Should().Be("1, 3");
		}

		[Test]
		public void TestConvert4()
		{
			_converter.Convert(new Int32[] { 1, 2, 3 }, null, null, null).Should().Be("1-3");
		}

		[Test]
		public void TestConvert5()
		{
			_converter.Convert(new Int32[] { 1, 2, 3, 5, 8, 9, 10, 11 }, null, null, null).Should().Be("1-3, 5, 8-11");
		}

		[Test]
		public void TestConvert6()
		{
			_converter.Convert(new Int32[] { -1, 0, 1, 2 }, null, null, null).Should().Be("-1-2");
		}

		[Test]
		public new void TestConvertBack1()
		{
			var values = _converter.ConvertBack("1", typeof(IEnumerable<Int32>), null, null) as IEnumerable<Int32>;
			values.Should().BeEquivalentTo(new Int32[] { 1 });
		}

		[Test]
		public new void TestConvertBack2()
		{
			var values = _converter.ConvertBack("1, 2", typeof(IEnumerable<Int32>), null, null) as IEnumerable<Int32>;
			values.Should().BeEquivalentTo(new Int32[] { 1, 2 });
		}

		[Test]
		public new void TestConvertBack3()
		{
			var values = _converter.ConvertBack("1-5", typeof(IEnumerable<Int32>), null, null) as IEnumerable<Int32>;
			values.Should().BeEquivalentTo(new Int32[] { 1, 2, 3, 4, 5 });
		}

		[Test]
		public new void TestConvertBack4()
		{
			new Action(() => _converter.ConvertBack("0-2147483647", typeof(IEnumerable<Int32>), null, null))
				.Should().NotThrow();
		}

		[Test]
		public new void TestConvertBack5()
		{
			var sw = new Stopwatch();
			sw.Start();
			var values = _converter.ConvertBack("0-9999", typeof(IEnumerable<Int32>), null, null) as IEnumerable<Int32>;
			sw.Stop();
			Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);

			values.Count().Should().Be(10000);
		}

		protected override IValueConverter Converter
		{
			get { return _converter; }
		}
	}
}