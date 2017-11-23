using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;
using WpfUnit;

namespace Metrolib.Test.Input
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class SuggestionInputControlTest
	{
		private Style _style;
		private SuggestionInputControl _control;
		private TestKeyboard _keyboard;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_style = (Style) App.Instance.FindResource(typeof(SuggestionInputControl));
		}

		[SetUp]
		public void Setup()
		{
			_control = new SuggestionInputControl
			{
				Style = _style
			};
			_control.ApplyTemplate().Should().BeTrue();
			_control.Popup.Should().NotBeNull();

			_keyboard = new TestKeyboard();
		}

		[Test]
		[Ignore("Can't be tested yet - popup refuses IsOpen to stay true - probably because no window is being shown")]
		[Description("Verifies that the popup opens when text has been entered and suggestions become available")]
		public void TestChangeText1()
		{
			_control.Popup.IsOpen.Should().BeFalse();

			_control.Text = "I feel";
			_control.Popup.IsOpen.Should().BeFalse();

			_control.Suggestions = new[] {"I feel a disturbance in the force"};
			_control.Popup.IsOpen.Should().BeTrue();
		}

		[Test]
		[Description("Verifies that the first available suggestion is selected by default")]
		public void TestChangeText2()
		{
			_control.SelectedSuggestion.Should().BeNull();
			_control.Text = "I feel";
			_control.Suggestions = new[] { "I feel a disturbance in the force" };
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);
		}

		[Test]
		public void TestSelectNextSuggestion1()
		{
			_control.Text = "I";
			_control.Suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);

			_keyboard.Press(_control, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(1);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[1]);

			_keyboard.Press(_control, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);
		}

		[Test]
		public void TestSelectNextSuggestion2()
		{
			_control.Text = "I";
			_control.Suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);

			_keyboard.Press(_control.TextBox, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(1);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[1]);

			_keyboard.Press(_control.TextBox, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);
		}

		[Test]
		public void TestSelectPreviousSuggestion1()
		{
			_control.Text = "I";
			_control.Suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);

			_keyboard.Press(_control, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(1);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[1]);

			_keyboard.Press(_control, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);
		}

		[Test]
		public void TestSelectPreviousSuggestion2()
		{
			_control.Text = "I";
			_control.Suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);

			_keyboard.Press(_control.TextBox, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(1);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[1]);

			_keyboard.Press(_control.TextBox, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);
		}

		[Test]
		public void TestChooseSuggestion1()
		{
			_control.Text = "I";
			_control.Suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.SelectedSuggestionIndex.Should().Be(0);
			_control.SelectedSuggestion.Should().Be(_control.Suggestions[0]);

			new Action(() => _keyboard.Press(_control, Key.Enter)).ShouldNotThrow(
				"because the control should expect that users forget to set the command");
		}

		[Test]
		public void TestChooseSuggestion2([Values(0, 1)] int selectedIndex)
		{
			_control.Text = "I";

			var suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestionIndex = selectedIndex;

			string chosenValue = null;
			_control.SuggestionChosenCommand = new DelegateCommand<string>(tmp =>
			{
				chosenValue = tmp;
			});

			_keyboard.Press(_control, Key.Enter);
			chosenValue.Should().Be(suggestions[selectedIndex]);
		}

		[Test]
		public void TestChooseSuggestion3([Values(0, 1)] int selectedIndex)
		{
			_control.Text = "I";

			var suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestionIndex = selectedIndex;

			string chosenValue = null;
			_control.SuggestionChosenCommand = new DelegateCommand<string>(tmp =>
			{
				chosenValue = tmp;
			});

			_keyboard.Press(_control.TextBox, Key.Enter);
			chosenValue.Should().Be(suggestions[selectedIndex]);
		}
	}
}