using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Converters
{
	public abstract class AbstractListToStringConverterTest<T>
	{
		protected abstract IValueConverter Converter { get; }

		[Test]
		public void TestConvertBack1()
		{
			Converter.ConvertBack(null, null, null, null).Should().BeNull();
		}

		[Test]
		public void TestConvertBack2()
		{
			var value = Converter.ConvertBack(null, typeof(IEnumerable<T>), null, null);
			value.Should().NotBeNull();
			value.Should().BeAssignableTo<IEnumerable<T>>();
			value.Should().BeOfType<List<T>>();
		}

		[Test]
		public void TestConvertBack3()
		{
			var value = Converter.ConvertBack(null, typeof(List<T>), null, null);
			value.Should().NotBeNull();
			value.Should().BeOfType<List<T>>();
		}

		[Test]
		public void TestConvertBack4()
		{
			var value = Converter.ConvertBack(null, typeof(T[]), null, null);
			value.Should().NotBeNull();
			value.Should().BeOfType<T[]>();
		}

		[Test]
		public void TestConvertBack5()
		{
			var value = Converter.ConvertBack(null, typeof(Collection<T>), null, null);
			value.Should().NotBeNull();
			value.Should().BeOfType<Collection<T>>();
		}

		[Test]
		public void TestConvertBack6()
		{
			Converter.ConvertBack(null, typeof(int), null, null).Should().BeNull();
			Converter.ConvertBack(null, typeof(float), null, null).Should().BeNull();
			Converter.ConvertBack(null, typeof(string), null, null).Should().BeNull();
		}

		public static IEnumerable<string> InvalidInput
		{
			get
			{
				return new[]
				{
					",",
					"-",
					"1-,2",
					"1,klingon,2",
					"foo",
					"a12",
					"Klingon"
				};
			}
		}

		[Test]
		public void TestConvertBack7([ValueSource(nameof(InvalidInput))] string input)
		{
			new Action(() => Converter.ConvertBack(input, typeof(T[]), null, null)).Should().NotThrow();
			Converter.ConvertBack(input, typeof(T[]), null, null).Should().Be(Binding.DoNothing);
		}
	}
}