using System;
using System.Collections.Generic;
using System.Windows.Data;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class UInt32ToStringConverterTest
		: AbstractNumberToStringConverterTest
	{
		private UInt32ToStringConverter _converter;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_converter = new UInt32ToStringConverter();
		}

		[Test]
		public new void TestConvert1()
		{
			_converter.Convert(42u, null, null, null).Should().Be("42");
		}
		
		[Test]
		public new void TestConvertBack2()
		{
			_converter.ConvertBack("42", typeof(UInt32), null, null).Should().Be(42u);
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
		public void TestConvertBack3([ValueSource(nameof(InvalidInputs))] string invalidInput)
		{
			_converter.ConvertBack(invalidInput, typeof(UInt32), null, null)
				.Should().Be(Binding.DoNothing, "because the user hasn't finished his input");
		}

		protected override IValueConverter Converter
		{
			get { return _converter; }
		}
	}
}