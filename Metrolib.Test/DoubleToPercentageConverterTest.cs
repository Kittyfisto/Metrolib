using System;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class DoubleToPercentageConverterTest
	{
		[SetUp]
		public void Setup()
		{
			_converter = new DoubleToPercentageConverter();
		}

		private DoubleToPercentageConverter _converter;

		[Test]
		public void TestConvert1()
		{
			new Action(() => _converter.Convert(null, null, null, null)).ShouldNotThrow();
			new Action(() => _converter.Convert("", null, null, null)).ShouldNotThrow();
			new Action(() => _converter.Convert(42, null, null, null)).ShouldNotThrow();
			new Action(() => _converter.Convert("42", null, null, null)).ShouldNotThrow();
			new Action(() => _converter.Convert(typeof(int), null, null, null)).ShouldNotThrow();
		}

		[Test]
		[SetCulture("en-US")]
		public void TestConvert2()
		{
			_converter.Convert(0.0, typeof(string), null, null).Should().Be("0%");
			_converter.Convert(1.0, typeof(string), null, null).Should().Be("100%");
			_converter.Convert(0.5, typeof(string), null, null).Should().Be("50%");
			_converter.Convert(0.55, typeof(string), null, null).Should().Be("55%");
			_converter.Convert(0.555, typeof(string), null, null).Should().Be("56%");
			_converter.Convert(0.5555, typeof(string), null, null).Should().Be("56%");
		}
	}
}