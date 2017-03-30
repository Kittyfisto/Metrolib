using System;
using System.Collections.Generic;
using System.Windows.Data;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class UInt32ListToStringConverterTest
		: AbstractListToStringConverterTest<UInt32>
	{
		private UInt32ListToStringConverter _converter;

		[TestFixtureSetUp]
		public void TestFixtureSetup()
		{
			_converter = new UInt32ListToStringConverter();
		}

		[Test]
		public void TestConvert1()
		{
			_converter.Convert(new UInt32[] {}, null, null, null).Should().Be("");
		}

		[Test]
		public void TestConvert2()
		{
			_converter.Convert(new UInt32[] { 1 }, null, null, null).Should().Be("1");
		}

		[Test]
		public void TestConvert3()
		{
			_converter.Convert(new UInt32[] { 1, 3 }, null, null, null).Should().Be("1, 3");
		}

		[Test]
		public void TestConvert4()
		{
			_converter.Convert(new UInt32[] { 1, 2, 3 }, null, null, null).Should().Be("1-3");
		}

		[Test]
		public void TestConvert5()
		{
			_converter.Convert(new UInt32[] { 1, 2, 3, 5, 8, 9, 10, 11 }, null, null, null).Should().Be("1-3, 5, 8-11");
		}

		[Test]
		public new void TestConvertBack1()
		{
			var values = _converter.ConvertBack("1", typeof(IEnumerable<UInt32>), null, null) as IEnumerable<UInt32>;
			values.Should().BeEquivalentTo(new UInt32[] {1});
		}

		[Test]
		public new void TestConvertBack2()
		{
			var values = _converter.ConvertBack("1, 2", typeof(IEnumerable<UInt32>), null, null) as IEnumerable<UInt32>;
			values.Should().BeEquivalentTo(new UInt32[] { 1, 2 });
		}

		[Test]
		public new void TestConvertBack3()
		{
			var values = _converter.ConvertBack("1-5", typeof(IEnumerable<UInt32>), null, null) as IEnumerable<UInt32>;
			values.Should().BeEquivalentTo(new UInt32[] {1, 2, 3, 4, 5});
		}

		protected override IValueConverter Converter
		{
			get { return _converter; }
		}
	}
}