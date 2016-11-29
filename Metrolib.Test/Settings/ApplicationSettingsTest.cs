using System;
using System.IO;
using System.Windows;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Settings
{
	[TestFixture]
	public sealed class ApplicationSettingsTest
	{
		[Test]
		public void TestClone1()
		{
			var settings = new ApplicationSettings
				{
					MainWindowSettings = new WindowSettings
						{
							Top = 1,
							Left = 2,
							Width = 3,
							Height = 4,
							State = WindowState.Minimized
						}
				};
			var clone = settings.Clone();
			clone.Should().NotBeNull();
			clone.Should().NotBeSameAs(settings);
			clone.MainWindowSettings.Should().NotBeNull();
			clone.MainWindowSettings.Should().NotBeSameAs(settings.MainWindowSettings);
			clone.MainWindowSettings.Top.Should().Be(1);
			clone.MainWindowSettings.Left.Should().Be(2);
			clone.MainWindowSettings.Width.Should().Be(3);
			clone.MainWindowSettings.Height.Should().Be(4);
			clone.MainWindowSettings.State.Should().Be(WindowState.Minimized);
		}

		[Test]
		public void TestClone2()
		{
			var settings = new ApplicationSettings
			{
				MainWindowSettings = null
			};
			var clone = settings.Clone();
			clone.Should().NotBeNull();
			clone.Should().NotBeSameAs(settings);
			clone.MainWindowSettings.Should().BeNull();
		}

		[Test]
		public void TestClone3()
		{
			var settings = new ApplicationSettings
			{
				MainWindowSettings = new WindowSettings
				{
					Top = 1,
					Left = 2,
					Width = 3,
					Height = 4,
					State = WindowState.Minimized
				}
			};
			var clone = ((ICloneable)settings).Clone();
			clone.Should().NotBeNull();
			clone.Should().NotBeSameAs(settings);
			clone.Should().BeOfType<ApplicationSettings>();

			var actual = (ApplicationSettings) clone;
			actual.MainWindowSettings.Should().NotBeNull();
			actual.MainWindowSettings.Should().NotBeSameAs(settings.MainWindowSettings);
			actual.MainWindowSettings.Top.Should().Be(1);
			actual.MainWindowSettings.Left.Should().Be(2);
			actual.MainWindowSettings.Width.Should().Be(3);
			actual.MainWindowSettings.Height.Should().Be(4);
			actual.MainWindowSettings.State.Should().Be(WindowState.Minimized);
		}

		[Test]
		[Description("Verifies that Save and RestoreFrom roundtrip")]
		public void TestSave1()
		{
			var settings = new ApplicationSettings
				{
					MainWindowSettings =
						{
							Top = 1,
							Left = 2,
							Height = 3,
							Width = 4,
							State = WindowState.Normal
						}
				};
			var fname = Path.GetTempFileName();
			new Action(() => settings.Save(fname)).ShouldNotThrow();

			var actual = new ApplicationSettings();
			actual.RestoreFrom(fname);
			actual.MainWindowSettings.Should().NotBeNull();
			actual.MainWindowSettings.Should().NotBeNull();
			actual.MainWindowSettings.Top.Should().Be(1);
			actual.MainWindowSettings.Left.Should().Be(2);
			actual.MainWindowSettings.Height.Should().Be(3);
			actual.MainWindowSettings.Width.Should().Be(4);
			actual.MainWindowSettings.State.Should().Be(WindowState.Normal);
		}

		[Test]
		[Description("Verifies that RestoreFrom() throws an appropriate exception when the directory doesn't exist")]
		public void TestRestoreFrom1()
		{
			var settings = new ApplicationSettings();
			var fname = Path.Combine(Path.GetTempPath(), "Hello Kitty", "foo.xml");
			new Action(() => settings.RestoreFrom(fname)).ShouldThrow<DirectoryNotFoundException>();
		}
	}
}