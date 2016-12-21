using System;
using System.Diagnostics.Contracts;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.ProgressBar
{
	[TestFixture]
	public abstract class AbstractProgressBarTest
	{
		[Pure]
		protected abstract AbstractProgressBar Create();

		[Test]
		[STAThread]
		public void TestCtor()
		{
			var bar = Create();
			bar.Minimum.Should().Be(0);
			bar.Maximum.Should().Be(100);
			bar.Value.Should().Be(0);
			bar.RelativeValue.Should().Be(0);
		}

		[Test]
		[STAThread]
		public void TestValue1()
		{
			var bar = Create();
			bar.Value = 50;
			bar.RelativeValue.Should().Be(0.5);
			bar.Value = 98;
			bar.RelativeValue.Should().Be(0.98);
		}

		[Test]
		[STAThread]
		public void TestMinimum1()
		{
			var bar = Create();
			bar.Minimum = -100;
			bar.RelativeValue.Should().Be(0.5);
			bar.Value = 50;
			bar.Minimum = 50;
			bar.RelativeValue.Should().Be(0);
		}

		[Test]
		[STAThread]
		public void TestMaximum1()
		{
			var bar = Create();
			bar.Value = 25;
			bar.Maximum = 50;
			bar.RelativeValue.Should().Be(0.5);
			bar.Value = 50;
			bar.Maximum = 200;
			bar.RelativeValue.Should().Be(0.25);
		}
	}
}