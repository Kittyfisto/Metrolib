using System.Windows.Media;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class ColorToBrushConverterTest
	{
		[Test]
		public void TestConvert1()
		{
			var converter = new ColorToBrushConverter();
			var value = converter.Convert(Colors.Red, typeof(Brush), null, null);
			value.Should().NotBeNull();
			value.Should().BeOfType<SolidColorBrush>();
			var brush = (SolidColorBrush) value;
			brush.Color.Should().Be(Colors.Red);
		}

		[Test]
		public void TestConvert2()
		{
			var converter = new ColorToBrushConverter();
			converter.Convert(null, typeof(Brush), null, null).Should().BeNull();
		}
	}
}