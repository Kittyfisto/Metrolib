using System;
using System.Windows;
using FluentAssertions;
using Metrolib.Settings;
using NUnit.Framework;

namespace Metrolib.Test.Settings
{
	[TestFixture]
	public sealed class WindowSettingsTest
	{
		[Test]
		public void TestClone1()
		{
			var window = new WindowSettings
				{
					Top = 1,
					Left = 2,
					Width = 3,
					Height = 4,
					State = WindowState.Maximized
				};
			var clone = window.Clone();
			clone.Should().NotBeNull();
			clone.Should().NotBeSameAs(window);
			clone.Top.Should().Be(1);
			clone.Left.Should().Be(2);
			clone.Width.Should().Be(3);
			clone.Height.Should().Be(4);
			clone.State.Should().Be(WindowState.Maximized);
		}

		[Test]
		public void TestClone2()
		{
			var window = new WindowSettings
			{
				Top = 1,
				Left = 2,
				Width = 3,
				Height = 4,
				State = WindowState.Maximized
			};
			var clone = ((ICloneable)window).Clone();
			clone.Should().NotBeNull();
			clone.Should().NotBeSameAs(window);
			clone.Should().BeOfType<WindowSettings>();

			var actual = (WindowSettings) clone;
			actual.Top.Should().Be(1);
			actual.Left.Should().Be(2);
			actual.Width.Should().Be(3);
			actual.Height.Should().Be(4);
			actual.State.Should().Be(WindowState.Maximized);
		}
	}
}