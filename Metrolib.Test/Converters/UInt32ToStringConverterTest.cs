using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class UInt32ToStringConverterTest
	{
		private UInt32ToStringConverter _converter;

		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_converter = new UInt32ToStringConverter();
		}

		[Test]
		public void TestConvert()
		{
			_converter.Convert(null, null, null, null).Should().Be(null);
			_converter.Convert(42u, null, null, null).Should().Be("42");
			_converter.Convert(42.12, null, null, null).Should().Be(null);
		}

		[Test]
		public void TestConvertBack()
		{
			_converter.ConvertBack(null, null, null, null).Should().Be(null);
			_converter.ConvertBack("42", null, null, null).Should().Be(42u);
			_converter.ConvertBack(42.12, null, null, null).Should().Be(null);
		}
	}
}