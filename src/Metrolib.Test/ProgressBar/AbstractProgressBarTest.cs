using System.Diagnostics.Contracts;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.ProgressBar
{
	[TestFixture]
	public abstract class AbstractProgressBarTest
		: AbstractControlTest
	{
		#region Overrides of AbstractControlTest

		protected override FrameworkElement Create()
		{
			return CreateProgressBar();
		}

		#endregion

		[Pure]
		protected abstract AbstractProgressBar CreateProgressBar();

		[Test]
		public void TestCtor()
		{
			var bar = CreateProgressBar();
			bar.Minimum.Should().Be(0);
			bar.Maximum.Should().Be(100);
			bar.Value.Should().Be(0);
			bar.RelativeValue.Should().Be(0);
		}

		[Test]
		public void TestMaximum1()
		{
			var bar = CreateProgressBar();
			bar.Value = 25;
			bar.Maximum = 50;
			bar.RelativeValue.Should().Be(0.5);
			bar.Value = 50;
			bar.Maximum = 200;
			bar.RelativeValue.Should().Be(0.25);
		}

		[Test]
		public void TestMinimum1()
		{
			var bar = CreateProgressBar();
			bar.Minimum = -100;
			bar.RelativeValue.Should().Be(0.5);
			bar.Value = 50;
			bar.Minimum = 50;
			bar.RelativeValue.Should().Be(0);
		}

		[Test]
		public void TestValue1()
		{
			var bar = CreateProgressBar();
			bar.Value = 50;
			bar.RelativeValue.Should().Be(0.5);
			bar.Value = 98;
			bar.RelativeValue.Should().Be(0.98);
		}
	}
}