using System.Reflection;
using System.Threading;
using System.Windows;
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
		private EditorTextBox _textBox;
		private MarkdownPresenter _presenter;

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

			var type = typeof(EditableTextBlock);
			_textBox = (EditorTextBox)type.GetField("_textBox", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_control);
			_presenter = (MarkdownPresenter)type.GetField("_presenter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_control);
		}

		[Test]
		public void TestCtor()
		{
			_control.Text.Should().BeNullOrEmpty();
			_control.Watermark.Should().BeNullOrEmpty();
			_control.TextOrWatermark.Should().BeNullOrEmpty();
		}

		[Test]
		public void TestApplyTemplate()
		{
			var control = new EditableTextBlock
			{
				Style = StyleHelper.Load<EditableTextBlock>(),
				Text = "Foobar"
			};
			control.ApplyTemplate();
			var type = typeof(EditableTextBlock);
			var presenter = (MarkdownPresenter)type.GetField("_presenter", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(control);
			presenter.Markdown.Should().Be("Foobar");
		}

		[Test]
		[Ignore("https://github.com/Kittyfisto/WpfUnit/issues/1")]
		public void TestEnableEditing()
		{
			_control.Text = "Hello, World!";

			_control.IsEditing.Should().BeFalse();
			_presenter.IsVisible.Should().BeTrue();
			_textBox.IsVisible.Should().BeFalse();
			_textBox.IsFocused.Should().BeFalse();

			_mouse.LeftClick(_presenter);
			_mouse.LeftClick(_presenter);

			_control.IsEditing.Should().BeTrue();
			// _control.Dispatcher.ExecuteAllPendingEvents();
			_presenter.IsVisible.Should().BeFalse();
			_textBox.IsVisible.Should().BeTrue("because enabling editing shall show the textbox");
			_textBox.IsFocused.Should().BeTrue();
			_textBox.SelectedText.Should().Be("Hello, World!", "because the entire text shall be selected by default");
		}

		[Test]
		public void TestChangeWatermark1()
		{
			_control.Watermark = "Give me characters!";
			_textBox.Watermark.Should().Be("Give me characters!");
		}

		[Test]
		public void TestChangeWatermark2()
		{
			_control.Watermark = "Foo";
			_control.Text.Should().BeNull();
			_control.TextOrWatermark.Should().Be("Foo");
			_presenter.Markdown.Should().Be("Foo");

			_control.Watermark = "Foobar";
			_control.Text.Should().BeNull();
			_control.TextOrWatermark.Should().Be("Foobar");
			_presenter.Markdown.Should().Be("Foobar");
		}

		[Test]
		public void TestChangeText()
		{
			_control.Text = "Foo";
			_control.TextOrWatermark.Should().Be("Foo");
			_presenter.Markdown.Should().Be("Foo");

			_control.Text = "Foobar";
			_control.TextOrWatermark.Should().Be("Foobar");
			_presenter.Markdown.Should().Be("Foobar");
		}

		[Test]
		public void TestChangeTextAndWatermark()
		{
			_control.Watermark = "Stuff";
			_control.TextOrWatermark.Should().Be("Stuff");
			_presenter.Markdown.Should().Be("Stuff");

			_control.Text = "";
			_control.Watermark.Should().Be("Stuff");
			_control.TextOrWatermark.Should().Be("Stuff");
			_presenter.Markdown.Should().Be("Stuff");

			_control.Text = "Foobar";
			_control.TextOrWatermark.Should().Be("Foobar");
			_presenter.Markdown.Should().Be("Foobar");
		}

		[Test]
		public void TestDisableEditing1()
		{
			_control.IsEditing.Should().BeFalse();
			_textBox.Visibility.Should().Be(Visibility.Hidden);

			_control.IsEditing = true;
			_textBox.Visibility.Should().Be(Visibility.Visible);

			_control.IsEditing = false;
			_textBox.Visibility.Should().Be(Visibility.Hidden);
		}

		[Test]
		public void TestDisableEditing2()
		{
			_control.IsEditing = true;

			_keyboard.Click(_textBox, Key.Escape);
			_control.IsEditing.Should().BeFalse("because pressing escape shall disable editing");
		}

		[Test]
		public void TestDisableEditing3()
		{
			_control.IsEditing = true;

			_keyboard.Click(_textBox, Key.Enter);
			_control.IsEditing.Should().BeFalse("because pressing enter shall disable editing");
		}

		[Test]
		[Description("Verifies that changes to the textbox are only ever reflected back to the text property when editing has concluded")]
		public void TestEdit1()
		{
			_control.Text = "Hello,";
			_control.IsEditing = true;
			_textBox.Text.Should().Be("Hello,");
			_textBox.Text = "Hello, World!";
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
			_textBox.Text.Should().Be("Hello,");
			_textBox.Text = "Hello, World!";
			_control.Text.Should().Be("Hello,", "because we haven't finished editing");

			_keyboard.Click(_textBox, Key.Enter);
			_control.Text.Should().Be("Hello, World!",
				"because we've finished editing and thus the edited value should've been written to the text property");
		}

		[Test]
		[Description("Verifies that the Text property remains unchanged when the user cancels editing with escape")]
		public void TestCancelEditing()
		{
			_control.Text = "stuff!";
			_control.IsEditing = true;
			_textBox.Text.Should().Be("stuff!");
			_textBox.Text = "some stuff!";
			_keyboard.Click(_textBox, Key.Escape);
			_control.Text.Should().Be("stuff!", "because we cancelled the user input and thus the text value should remain unchanged");
		}

		[Test]
		[Description("Verifies that both textbox and -block share the same padding")]
		public void TestPadding()
		{
			_control.Padding.Should().Be(new Thickness(4));
			_textBox.Padding.Should().Be(new Thickness(4));
			_presenter.Padding.Should().Be(new Thickness(4));

			_control.Padding = new Thickness(5);
			_textBox.Padding.Should().Be(new Thickness(5));
			_presenter.Padding.Should().Be(new Thickness(5));
		}
	}
}