using System;
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

		[TestFixtureSetUp]
		public void TestFixtureSetup()
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

		protected override IValueConverter Converter
		{
			get { return _converter; }
		}
	}
}