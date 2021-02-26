using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class NullToFalseConverterTest
	{
		[Test]
		public void TestConvert()
		{
			var converter = new NullToFalseConverter();
			converter.Convert(null, null, null, null).Should().Be(false);
			converter.Convert(true, null, null, null).Should().Be(true);
			converter.Convert(42, null, null, null).Should().Be(true);
			converter.Convert(false, null, null, null).Should().Be(true);
		}
	}
}
