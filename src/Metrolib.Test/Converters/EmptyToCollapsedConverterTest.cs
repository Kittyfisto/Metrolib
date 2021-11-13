using System.Windows;
using System.Windows.Data;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class EmptyToCollapsedConverterTest
	{
		private EmptyToCollapsedConverter _converter;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_converter = new EmptyToCollapsedConverter();
		}

		[Test]
		public void TestConvert1()
		{
			_converter.Convert(null, null, null, null).Should().BeNull();
		}

		[Test]
		public void TestConvert2()
		{
			_converter.Convert(new object[0], null, null, null).Should().Be(Visibility.Collapsed);
		}

		[Test]
		public void TestConvert3()
		{
			_converter.Convert(new[] { 1 }, null, null, null).Should().Be(Visibility.Visible);
		}

		[Test]
		public void TestConvert4()
		{
			_converter.Convert("", null, null, null).Should().Be(Visibility.Collapsed);
		}

		[Test]
		public void TestConvert5()
		{
			_converter.Convert("a", null, null, null).Should().Be(Visibility.Visible);
		}

		[Test]
		public void TestConvertBack()
		{
			_converter.ConvertBack(null, null, null, null).Should().Be(Binding.DoNothing);
		}
	}
}
