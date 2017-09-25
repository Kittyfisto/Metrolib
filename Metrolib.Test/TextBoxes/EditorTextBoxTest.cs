using System.Threading;
using System.Windows.Input;
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
		public void TestToggleBoldness()
		{
			_textBox.Text = "Foobar";
			_textBox.SelectedText = "Foobar";

			_keyboard.Press(Key.LeftCtrl);
			_keyboard.Click(_textBox, Key.B);
		}
	}
}