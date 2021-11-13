using System.Collections.Generic;
using System.Windows.Data;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class ByteToStringConverterTest
		: AbstractNumberToStringConverterTest
	{
		private ByteToStringConverter _converter;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_converter = new ByteToStringConverter();
		}

		[Test]
		public new void TestConvert1()
		{
			_converter.Convert((byte)42, null, null, null).Should().Be("42");
		}

		[Test]
		public new void TestConvertBack2()
		{
			_converter.ConvertBack("42", typeof(byte), null, null).Should().Be((byte)42);
		}

		[Test]
		public void TestConvertBack3()
		{
			_converter.ConvertBack("42", typeof(byte?), null, null).Should().Be((byte)42);
		}

		public static IEnumerable<string> InvalidInputs
		{
			get
			{
				return new[]
				{
					"",
					"f",
					"42.",
					".1211"
				};
			}
		}

		[Test]
		public void TestConvertBack4([ValueSource(nameof(InvalidInputs))] string invalidInput)
		{
			_converter.ConvertBack(invalidInput, typeof(byte), null, null)
				.Should().Be(Binding.DoNothing, "because the user hasn't finished his input");
		}

		protected override IValueConverter Converter
		{
			get { return _converter; }
		}
	}
}