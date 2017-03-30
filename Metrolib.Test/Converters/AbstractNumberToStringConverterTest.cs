using System.Windows.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	public abstract class AbstractNumberToStringConverterTest
	{
		protected abstract IValueConverter Converter { get; }

		[Test]
		public void TestConvert1()
		{
			Converter.Convert(null, null, null, null).Should().Be(null);
			Converter.Convert(42.12, null, null, null).Should().Be(null);
		}

		[Test]
		public void TestConvertBack2()
		{
			Converter.ConvertBack(null, null, null, null).Should().Be(null);
			Converter.ConvertBack(42.12, null, null, null).Should().Be(null);
		}

	}
}