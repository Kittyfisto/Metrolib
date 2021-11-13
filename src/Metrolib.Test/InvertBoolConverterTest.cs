using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class InvertBoolConverterTest
	{
		[Test]
		public void TestConvert1()
		{
			var converter = new InvertBoolConverter();
			converter.Convert(true, typeof(bool), null, null).Should().Be(false);
			converter.Convert(false, typeof(bool), null, null).Should().Be(true);
		}

		[Test]
		public void TestConvert2()
		{
			var converter = new InvertBoolConverter();
			converter.Convert(42, typeof (bool), null, null).Should().BeNull();
			converter.Convert("false", typeof(bool), null, null).Should().BeNull();
			converter.Convert("true", typeof(bool), null, null).Should().BeNull();
			converter.Convert("clondyke bar", typeof (bool), null, null).Should().BeNull();
		}


		[Test]
		public void TestConvertBack1()
		{
			var converter = new InvertBoolConverter();
			converter.ConvertBack(true, typeof(bool), null, null).Should().Be(false);
			converter.ConvertBack(false, typeof(bool), null, null).Should().Be(true);
		}

		[Test]
		public void TestConvertBack2()
		{
			var converter = new InvertBoolConverter();
			converter.ConvertBack(42, typeof (bool), null, null).Should().BeNull();
			converter.ConvertBack("false", typeof(bool), null, null).Should().BeNull();
			converter.ConvertBack("true", typeof(bool), null, null).Should().BeNull();
			converter.ConvertBack("clondyke bar", typeof(bool), null, null).Should().BeNull();
		}
	}
}