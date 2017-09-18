using System.Threading;
using System.Windows.Input;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;
using WpfUnit;

namespace Metrolib.Test.TextBlocks
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class EditableTextBlockTest
	{
		private EditableTextBlock _control;
		private TestMouse _mouse;
		private TestKeyboard _keyboard;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_mouse = new TestMouse();
			_keyboard = new TestKeyboard();
		}

		[SetUp]
		public void Setup()
		{
			_control = new EditableTextBlock
			{
				Style = StyleHelper.Load<EditableTextBlock>()
			};
			_control.ApplyTemplate();
		}

		[Test]
		[Ignore("https://github.com/Kittyfisto/WpfUnit/issues/1")]
		public void TestEnableEditing()
		{
			_control.Text = "Hello, World!";

			_control.IsEditing.Should().BeFalse();
			_control.TextBlock.IsVisible.Should().BeTrue();
			_control.TextBox.IsVisible.Should().BeFalse();
			_control.TextBox.IsFocused.Should().BeFalse();

			_mouse.LeftClick(_control.TextBlock);
			_mouse.LeftClick(_control.TextBlock);

			_control.IsEditing.Should().BeTrue();
			// _control.Dispatcher.ExecuteAllPendingEvents();
			_control.TextBlock.IsVisible.Should().BeFalse();
			_control.TextBox.IsVisible.Should().BeTrue("because enabling editing shall show the textbox");
			_control.TextBox.IsFocused.Should().BeTrue();
			_control.TextBox.SelectedText.Should().Be("Hello, World!", "because the entire text shall be selected by default");
		}

		[Test]
		[Ignore("https://github.com/Kittyfisto/WpfUnit/issues/2")]
		public void TestDisableEditing1()
		{
			_control.IsEditing = true;
			// _control.Dispatcher.ExecuteAllPendingEvents();
			_control.TextBox.IsVisible.Should().BeTrue();

			_control.IsEditing = false;
			_control.TextBox.IsVisible.Should().BeFalse();
		}

		[Test]
		public void TestDisableEditing2()
		{
			_control.IsEditing = true;

			_keyboard.Click(_control.TextBox, Key.Escape);
			_control.IsEditing.Should().BeFalse("because pressing escape shall disable editing");
		}

		[Test]
		public void TestDisableEditing3()
		{
			_control.IsEditing = true;

			_keyboard.Click(_control.TextBox, Key.Enter);
			_control.IsEditing.Should().BeFalse("because pressing enter shall disable editing");
		}

		[Test]
		[Description("Verifies that changes to the textbox are only ever reflected back to the text property when editing has concluded")]
		public void TestEdit1()
		{
			_control.Text = "Hello,";
			_control.IsEditing = true;
			_control.TextBox.Text.Should().Be("Hello,");
			_control.TextBox.Text = "Hello, World!";
			_control.Text.Should().Be("Hello,", "because we haven't finished editing");

			_control.IsEditing = false;
			_control.Text.Should().Be("Hello, World!",
				"because we've finished editing and thus the edited value should've been written to the text property");
		}

		[Test]
		[Description("Verifies that changes to the textbox are only ever reflected back to the text property when editing has concluded")]
		public void TestEdit2()
		{
			_control.Text = "Hello,";
			_control.IsEditing = true;
			_control.TextBox.Text.Should().Be("Hello,");
			_control.TextBox.Text = "Hello, World!";
			_control.Text.Should().Be("Hello,", "because we haven't finished editing");

			_keyboard.Click(_control.TextBox, Key.Enter);
			_control.Text.Should().Be("Hello, World!",
				"because we've finished editing and thus the edited value should've been written to the text property");
		}

		[Test]
		[Description("Verifies that the Text property remains unchanged when the user cancels editing with escape")]
		public void TestCancelEditing()
		{
			_control.Text = "stuff!";
			_control.IsEditing = true;
			_control.TextBox.Text.Should().Be("stuff!");
			_control.TextBox.Text = "some stuff!";
			_keyboard.Click(_control.TextBox, Key.Escape);
			_control.Text.Should().Be("stuff!", "because we cancelled the user input and thus the text value should remain unchanged");
		}
	}
}