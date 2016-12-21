using System;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.ProgressBar
{
	[TestFixture]
	public sealed class FlatProgressBarTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var bar = new FlatProgressBar();
			bar.Minimum.Should().Be(0);
			bar.Maximum.Should().Be(100);
			bar.Value.Should().Be(0);
		}
	}
}