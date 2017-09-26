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
			_textBox = new EditorTextBox();
		}

		[Test]
		public void TestToggleBoldness1()
		{
			_textBox.Text = "Foobar";
			_textBox.Select(0, 6);

			_keyboard.Press(Key.LeftCtrl);
			_keyboard.Click(_textBox, Key.B);

			_textBox.Text.Should().Be("**Foobar**");
			_textBox.SelectedText.Should().Be("**Foobar**");
		}

		[Test]
		public void TestToggleBoldness2()
		{
			_textBox.Text = "**Foobar**";
			_textBox.Select(0, 10);

			_keyboard.Press(Key.LeftCtrl);
			_keyboard.Click(_textBox, Key.B);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleBoldness3()
		{
			_textBox.Text = "__Foobar__";
			_textBox.Select(0, 10);

			_keyboard.Press(Key.LeftCtrl);
			_keyboard.Click(_textBox, Key.B);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleItalic1()
		{
			_textBox.Text = "Foobar";
			_textBox.Select(0, 6);

			_keyboard.Press(Key.LeftCtrl);
			_keyboard.Click(_textBox, Key.I);

			_textBox.Text.Should().Be("*Foobar*");
			_textBox.SelectedText.Should().Be("*Foobar*");
		}

		[Test]
		public void TestToggleItalic2()
		{
			_textBox.Text = "*Foobar*";
			_textBox.Select(0, 8);

			_keyboard.Press(Key.LeftCtrl);
			_keyboard.Click(_textBox, Key.I);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}

		[Test]
		public void TestToggleItalic3()
		{
			_textBox.Text = "_Foobar_";
			_textBox.Select(0, 8);

			_keyboard.Press(Key.LeftCtrl);
			_keyboard.Click(_textBox, Key.I);

			_textBox.Text.Should().Be("Foobar");
			_textBox.SelectedText.Should().Be("Foobar");
		}
	}
}