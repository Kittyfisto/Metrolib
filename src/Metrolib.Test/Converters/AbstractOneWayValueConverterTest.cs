using System;
using System.Windows.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	public abstract class AbstractOneWayValueConverterTest
	{
		public abstract Type SourceType { get; }

		protected abstract IValueConverter Converter { get; }

		protected abstract object SourceValue { get; }
		protected abstract object TargetValue { get; }

		[Test]
		public void TestConvertBack()
		{
			Converter.ConvertBack(TargetValue, SourceType, null, null)
				.Should().Be(Binding.DoNothing);
		}
	}
}