using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Windows;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.Input
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class SuggestionInputControlTest
	{
		[SetUp]
		public void Setup()
		{
			_control = new SuggestionInputControl
			{
				Style = _style
			};
			_control.ApplyTemplate().Should().BeTrue();
			_control.Popup.Should().NotBeNull();

		}

		private Style _style;
		private SuggestionInputControl _control;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_style = (Style) App.Instance.FindResource(typeof(SuggestionInputControl));
		}

		private sealed class ObservableCollectionMock
			: IEnumerable<object>
				, INotifyCollectionChanged
		{
			private readonly List<NotifyCollectionChangedEventHandler> _listeners;

			public IReadOnlyList<NotifyCollectionChangedEventHandler> Listeners => _listeners;

			public ObservableCollectionMock()
			{
				_listeners = new List<NotifyCollectionChangedEventHandler>();
			}

			public IEnumerator<object> GetEnumerator()
			{
				return new List<object>().GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public event NotifyCollectionChangedEventHandler CollectionChanged
			{
				add { _listeners.Add(value); }
				remove { _listeners.Remove(value); }
			}
		}

		[Test]
		[Description("It should be possible to change suggestions without having a style applied yet")]
		public void TestChangeSuggestions1()
		{
			var control = new SuggestionInputControl();
			new Action(() => control.Suggestions = new List<object>())
				.Should().NotThrow();
		}

		[Test]
		[Description("Verifies that the control subs and unsubs when the collection is replaced")]
		public void TestChangeSuggestions2()
		{
			var control = new SuggestionInputControl();
			var suggestions = new ObservableCollectionMock();
			suggestions.Listeners.Should().BeEmpty();

			control.Suggestions = suggestions;
			suggestions.Listeners.Should().HaveCount(1);

			control.Suggestions = null;
			suggestions.Listeners.Should().BeEmpty();
		}

		[Test]
		[Description("Verifies that the control unsubs when unloaded")]
		public void TestChangeSuggestions3()
		{
			var control = new SuggestionInputControl();
			var suggestions = new ObservableCollectionMock();
			suggestions.Listeners.Should().BeEmpty();

			control.Suggestions = suggestions;
			suggestions.Listeners.Should().HaveCount(1);

			control.RaiseEvent(new RoutedEventArgs(FrameworkElement.UnloadedEvent));
			suggestions.Listeners.Should().BeEmpty();
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
			var suggestions = new[] {"I feel a disturbance in the force"};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestion.Should().Be(suggestions[0]);
		}

		[Test]
		public void TestChooseSuggestion1()
		{
			_control.Text = "I";
			var suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);
		}

		[Test]
        [Ignore("WPF Unit hasn't been ported to dotnet")]
		public void TestChooseSuggestion2([Values(arg1: 0, arg2: 1)] int selectedIndex)
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
			_control.SuggestionChosenCommand = new DelegateCommand<string>(tmp => { chosenValue = tmp; });

			//_keyboard.Press(_control, Key.Enter);
			chosenValue.Should().Be(suggestions[selectedIndex]);
		}

		[Test]
        [Ignore("WPF Unit hasn't been ported to dotnet")]
		public void TestChooseSuggestion3([Values(arg1: 0, arg2: 1)] int selectedIndex)
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
			_control.SuggestionChosenCommand = new DelegateCommand<string>(tmp => { chosenValue = tmp; });

			//_keyboard.Press(_control.TextBox, Key.Enter);
			chosenValue.Should().Be(suggestions[selectedIndex]);
		}

		[Test]
		[Ignore("WPF Unit hasn't been ported to dotnet")]
		public void TestSelectNextSuggestion1()
		{
			_control.Text = "I";
			var suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);

			//_keyboard.Press(_control, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(expected: 1);
			_control.SelectedSuggestion.Should().Be(suggestions[1]);

			//_keyboard.Press(_control, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);
		}

		[Test]
        [Ignore("WPF Unit hasn't been ported to dotnet")]
		public void TestSelectNextSuggestion2()
		{
			_control.Text = "I";
			var suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);

			//_keyboard.Press(_control.TextBox, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(expected: 1);
			_control.SelectedSuggestion.Should().Be(suggestions[1]);

			//_keyboard.Press(_control.TextBox, Key.Down);
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);
		}

		[Test]
        [Ignore("WPF Unit hasn't been ported to dotnet")]
		public void TestSelectPreviousSuggestion1()
		{
			_control.Text = "I";
			var suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);

			//_keyboard.Press(_control, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(expected: 1);
			_control.SelectedSuggestion.Should().Be(suggestions[1]);

			//_keyboard.Press(_control, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);
		}

		[Test]
        [Ignore("WPF Unit hasn't been ported to dotnet")]
		public void TestSelectPreviousSuggestion2()
		{
			_control.Text = "I";
			var suggestions = new[]
			{
				"I'm Groot",
				"I'm awesome!"
			};
			_control.Suggestions = suggestions;
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);

			//_keyboard.Press(_control.TextBox, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(expected: 1);
			_control.SelectedSuggestion.Should().Be(suggestions[1]);

			//_keyboard.Press(_control.TextBox, Key.Up);
			_control.SelectedSuggestionIndex.Should().Be(expected: 0);
			_control.SelectedSuggestion.Should().Be(suggestions[0]);
		}
	}
}