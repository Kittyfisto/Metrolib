using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class SizeTest
	{
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
	}
}