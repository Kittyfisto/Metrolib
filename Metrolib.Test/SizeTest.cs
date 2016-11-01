using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class SizeTest
	{
		[Test]
		public void TestConstants()
		{
			Size.Zero.Bytes.Should().Be(0);
			Size.OneByte.Bytes.Should().Be(1);
			Size.OneKilobyte.Bytes.Should().Be(1024);
			Size.OneMegabyte.Bytes.Should().Be(1024 * 1024);
			Size.OneGigabyte.Bytes.Should().Be(1024*1024*1024);
		}

		[Test]
		public void TestFromBytes()
		{
			Size.FromBytes(42).Bytes.Should().Be(42);
		}

		[Test]
		public void TestFromKilobytes()
		{
			Size.FromKilobytes(42).Kilobytes.Should().Be(42);
			Size.FromKilobytes(42).Bytes.Should().Be(42 * 1024);
		}

		[Test]
		public void TestFromMegabytes()
		{
			Size.FromMegabytes(42).Megabytes.Should().Be(42);
			Size.FromMegabytes(42).Bytes.Should().Be(42*1024*1024);
		}

		[Test]
		public void TestMultiply()
		{
			(Size.FromBytes(2) * 3).Bytes.Should().Be(6);
			(3*Size.FromBytes(2)).Bytes.Should().Be(6);
		}

		[Test]
		public void TestSubtract()
		{
			(Size.FromMegabytes(2) - Size.OneMegabyte).Should().Be(Size.FromMegabytes(1));
		}

		[Test]
		public void TestDivide()
		{
			(Size.FromBytes(42)/10).Bytes.Should().Be(4);
		}

		[Test]
		public void TestEquality()
		{
			Size.Zero.Should().Be(Size.Zero);
			Size.OneByte.Should().Be(Size.OneByte);
			Size.OneMegabyte.Should().NotBe(Size.FromBytes(1023));
		}
	}
}