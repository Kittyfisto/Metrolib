using System.Collections.Generic;
using System.Windows.Data;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class UInt64ToStringConverterTest
		: AbstractNumberToStringConverterTest
	{
		private UInt64ToStringConverter _converter;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_converter = new UInt64ToStringConverter();
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

		protected override IValueConverter Converter
		{
			get { return _converter; }
		}

		[Test]
		public new void TestConvert1()
		{
			_converter.Convert((ulong) 42, null, null, null).Should().Be("42");
		}

		[Test]
		public new void TestConvertBack2()
		{
			_converter.ConvertBack("42", typeof(ulong), null, null).Should().Be((ulong) 42);
		}

		[Test]
		public void TestConvertBack3()
		{
			_converter.ConvertBack("42", typeof(ulong?), null, null).Should().Be((ulong) 42);
		}

		[Test]
		public void TestConvertBack4([ValueSource(nameof(InvalidInputs))] string invalidInput)
		{
			_converter.ConvertBack(invalidInput, typeof(ulong), null, null)
				.Should().Be(Binding.DoNothing, "because the user hasn't finished his input");
		}
	}
}