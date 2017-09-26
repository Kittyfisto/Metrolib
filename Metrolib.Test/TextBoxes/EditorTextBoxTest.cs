using System.Threading;
using System.Windows.Input;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;
using WpfUnit;

namespace Metrolib.Test.TextBoxes
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class EditorTextBoxTest
	{
		private EditorTextBox _textBox;
		private TestKeyboard _keyboard;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_keyboard = new TestKeyboard();
		}

		[SetUp]
		public void Setup()
		{
			// We'll just reset all keys to their default (non-pressed) state in case
			// a test forgot to do so...
			_keyboard.Reset();

			_textBox = new EditorTextBox();
		}

		[Test]
		public void TestCtor()
		{
			_textBox.Text.Should().BeNullOrEmpty();
			_textBox.EnableMarkdownShortcuts.Should().BeFalse();
		}

		[Test]
		public void TestToggleBoldness1()
		{
			_textBox.EnableMarkdownShortcuts = true;
			_textBox.Text = "Foobar";
			_textBox.Select(0, 6);

			_keyboard.Click(_textBox, Key.B, ModifierKeys.Control);

			_textBox.Text.Should().Be("**Foobar**");
			_textBox.SelectedText.Should().Be("**Foobar**");
		}

		[Test]
		public void TestToggleBoldness2()
		{
			_textBox.EnableMarkdownShortcuts = true;
			_textBox.Text = "**Foobar**";
			_textBox.Select(0, 10);

			_keyboard.Click(_textBox, Key.B, ModifierKeys.Control);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleBoldness3()
		{
			_textBox.EnableMarkdownShortcuts = true;
			_textBox.Text = "__Foobar__";
			_textBox.Select(0, 10);

			_keyboard.Click(_textBox, Key.B, ModifierKeys.Control);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleBoldness4()
		{
			_textBox.Text = "Foobar";
			_textBox.Select(0, 10);

			_keyboard.Click(_textBox, Key.B, ModifierKeys.Control);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleBoldness5()
		{
			_textBox.Text = "**Foobar**";
			_textBox.Select(0, 10);

			_keyboard.Click(_textBox, Key.B, ModifierKeys.Control);

			_textBox.Text.Should().Be("**Foobar**");
			_textBox.SelectedText.Should().Be("**Foobar**");
		}

		[Test]
		public void TestToggleItalic1()
		{
			_textBox.EnableMarkdownShortcuts = true;
			_textBox.Text = "Foobar";
			_textBox.Select(0, 6);

			_keyboard.Click(_textBox, Key.I, ModifierKeys.Control);

			_textBox.Text.Should().Be("*Foobar*");
			_textBox.SelectedText.Should().Be("*Foobar*");
		}

		[Test]
		public void TestToggleItalic2()
		{
			_textBox.EnableMarkdownShortcuts = true;
			_textBox.Text = "*Foobar*";
			_textBox.Select(0, 8);

			_keyboard.Click(_textBox, Key.I, ModifierKeys.Control);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleItalic3()
		{
			_textBox.EnableMarkdownShortcuts = true;
			_textBox.Text = "_Foobar_";
			_textBox.Select(0, 8);

			_keyboard.Click(_textBox, Key.I, ModifierKeys.Control);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleItalic4()
		{
			_textBox.EnableMarkdownShortcuts = false;
			_textBox.Text = "Foobar";
			_textBox.Select(0, 8);

			_keyboard.Click(_textBox, Key.I, ModifierKeys.Control);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleItalic5()
		{
			_textBox.EnableMarkdownShortcuts = false;
			_textBox.Text = "_Foobar_";
			_textBox.Select(0, 8);

			_keyboard.Click(_textBox, Key.I, ModifierKeys.Control);

			_textBox.Text.Should().Be("_Foobar_");
			_textBox.SelectedText.Should().Be("_Foobar_");
		}
	}
}