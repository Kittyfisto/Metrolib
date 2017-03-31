using System;
using System.Windows;
using System.Windows.Data;
using FluentAssertions;
using Metrolib.Converters;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	[TestFixture]
	public sealed class BoolFalseToHiddenConverterTest
		: AbstractOneWayValueConverterTest
	{
		private BoolFalseToHiddenConverter _converter;

		[SetUp]
		public void Setup()
		{
			_converter = new BoolFalseToHiddenConverter();
		}

		[Test]
		public void TestConvert1()
		{
			_converter.Convert(false, typeof(Visibility), null, null).Should().Be(Visibility.Hidden);
		}

		[Test]
		public void TestConvert2()
		{
			_converter.Convert(true, typeof(Visibility), null, null).Should().Be(Visibility.Visible);
		}
		
		public override Type SourceType
		{
			get { return typeof(bool); }
		}

		protected override IValueConverter Converter
		{
			get { return _converter; }
		}

		protected override object SourceValue
		{
			get { return true; }
		}

		protected override object TargetValue
		{
			get { return Visibility.Visible; }
		}
	}
}