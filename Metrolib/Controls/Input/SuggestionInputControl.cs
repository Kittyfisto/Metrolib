using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A control which allows the user to input single-line text
	///     which then displays a list of suggesstions from which the user
	///     can select some, or continue typing...
	/// </summary>
	/// <example>
	///     A view model should react to changes of the <see cref="Text" /> property and then provide
	///     a list of suggestions to this control via the <see cref="Suggestions" /> property.
	///     If the user selects a particular suggestions, then <see cref="SelectedSuggestion" /> is changed.
	/// </example>
	[TemplatePart(Name = PART_SuggestionPopup, Type = typeof(Popup))]
	[TemplatePart(Name = PART_SuggestionTextBox, Type = typeof(TextBox))]
	public sealed class SuggestionInputControl
		: Control
	{
		/// <summary>
		/// </summary>
		public const string PART_SuggestionPopup = "PART_SuggestionPopup";

		/// <summary>
		/// </summary>
		public const string PART_SuggestionTextBox = "PART_SuggestionTextBox";

		/// <summary>
		///     Definition of the <see cref="SuggestionTemplate" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionTemplateProperty = DependencyProperty.Register(
		                                                                                                   "SuggestionTemplate",
		                                                                                                   typeof(DataTemplate
		                                                                                                   ),
		                                                                                                   typeof(
			                                                                                                   SuggestionInputControl
		                                                                                                   ),
		                                                                                                   new
			                                                                                                   PropertyMetadata(propertyChangedCallback
			                                                                                                                    : null))
			;

		/// <summary>
		///     Definition of the <see cref="Text" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string),
		                                                                                     typeof(SuggestionInputControl),
		                                                                                     new
			                                                                                     FrameworkPropertyMetadata(defaultValue: null,
			                                                                                                               flags:
			                                                                                                               FrameworkPropertyMetadataOptions
				                                                                                                               .BindsTwoWayByDefault))
			;

		/// <summary>
		///     Definition of the <see cref="Watermark" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
		                                                                                          "Watermark", typeof(string),
		                                                                                          typeof(
			                                                                                          SuggestionInputControl),
		                                                                                          new
			                                                                                          PropertyMetadata(propertyChangedCallback
			                                                                                                           : null));

		/// <summary>
		///     Definition of the <see cref="Suggestions" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionsProperty = DependencyProperty.Register(
		                                                                                            "Suggestions",
		                                                                                            typeof(IEnumerable<
			                                                                                            object>),
		                                                                                            typeof(
			                                                                                            SuggestionInputControl),
		                                                                                            new
			                                                                                            PropertyMetadata(defaultValue: null,
			                                                                                                             propertyChangedCallback
			                                                                                                             : OnSuggestionsChanged))
			;

		/// <summary>
		///     Definition of the <see cref="SelectedSuggestion" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SelectedSuggestionProperty = DependencyProperty.Register(
		                                                                                                   "SelectedSuggestion",
		                                                                                                   typeof(object),
		                                                                                                   typeof(
			                                                                                                   SuggestionInputControl
		                                                                                                   ),
		                                                                                                   new
			                                                                                                   FrameworkPropertyMetadata(defaultValue: null,
			                                                                                                                             flags
			                                                                                                                             : FrameworkPropertyMetadataOptions
				                                                                                                                             .BindsTwoWayByDefault))
			;

		/// <summary>
		///     Definition of the <see cref="SelectedSuggestionIndex" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SelectedSuggestionIndexProperty = DependencyProperty.Register(
		                                                                                                        "SelectedSuggestionIndex",
		                                                                                                        typeof(int),
		                                                                                                        typeof(
			                                                                                                        SuggestionInputControl
		                                                                                                        ),
		                                                                                                        new
			                                                                                                        FrameworkPropertyMetadata(defaultValue: 0,
			                                                                                                                                  flags
			                                                                                                                                  : FrameworkPropertyMetadataOptions
				                                                                                                                                  .BindsTwoWayByDefault))
			;

		/// <summary>
		///     Definition of the <see cref="SelectedSuggestionIndex" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionChosenCommandProperty = DependencyProperty.Register(
		                                                                                                        "SuggestionChosenCommand",
		                                                                                                        typeof(
			                                                                                                        ICommand),
		                                                                                                        typeof(
			                                                                                                        SuggestionInputControl
		                                                                                                        ),
		                                                                                                        new
			                                                                                                        PropertyMetadata(default
			                                                                                                                         (ICommand
			                                                                                                                         )))
			;

		/// <summary>
		///     Definition of the <see cref="SuggestionTemplateSelector" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty SuggestionTemplateSelectorProperty = DependencyProperty.Register(
		                                                                                                           "SuggestionTemplateSelector",
		                                                                                                           typeof(
			                                                                                                           DataTemplateSelector
		                                                                                                           ),
		                                                                                                           typeof(
			                                                                                                           SuggestionInputControl
		                                                                                                           ),
		                                                                                                           new
			                                                                                                           PropertyMetadata(propertyChangedCallback
			                                                                                                                            : null))
			;

		/// <summary>
		///     Definition of the <see cref="IsThinking" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsThinkingProperty = DependencyProperty.Register(
		                                                                                           "IsThinking", typeof(bool),
		                                                                                           typeof(
			                                                                                           SuggestionInputControl),
		                                                                                           new
			                                                                                           PropertyMetadata(default(
				                                                                                                            bool)));

		private readonly KeyBinding _downBinding;
		private readonly KeyBinding _upBinding;
		private readonly KeyBinding _enterBinding;

		static SuggestionInputControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SuggestionInputControl),
			                                         new FrameworkPropertyMetadata(typeof(SuggestionInputControl)));
		}

		/// <summary>
		///     Initializes this object.
		/// </summary>
		public SuggestionInputControl()
		{
			_upBinding = new KeyBinding(new DelegateCommand2(OnKeyUp), Key.Up, ModifierKeys.None);
			_downBinding = new KeyBinding(new DelegateCommand2(OnKeyDown), Key.Down, ModifierKeys.None);
			_enterBinding = new KeyBinding(new DelegateCommand2(OnEnter), Key.Enter, ModifierKeys.None);

			InputBindings.Add(_upBinding);
			InputBindings.Add(_downBinding);
			InputBindings.Add(_enterBinding);

			GotFocus += OnGotFocus;
			Loaded += OnLoaded;
			Unloaded += OnUnloaded;
		}

		private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Subscribe(Suggestions);
		}

		private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
		{
			Unsubscribe(Suggestions);
		}

		private void OnGotFocus(object sender, RoutedEventArgs routedEventArgs)
		{
			TextBox?.Focus();
		}

		/// <summary>
		///     When set to true, the user is lead to believe that the software is trying
		///     to come up with suggestions.
		/// </summary>
		/// <remarks>
		///     Set this to true while you're looking for suggestions and then set it to false again.
		/// </remarks>
		public bool IsThinking
		{
			get { return (bool) GetValue(IsThinkingProperty); }
			set { SetValue(IsThinkingProperty, value); }
		}

		/// <summary>
		///     This command is <see cref="ICommand.Execute(object)" />d when the user actually decides to use a particular
		///     suggestion.
		/// </summary>
		public ICommand SuggestionChosenCommand
		{
			get { return (ICommand) GetValue(SuggestionChosenCommandProperty); }
			set { SetValue(SuggestionChosenCommandProperty, value); }
		}

		/// <summary>
		///     The template with which a suggestions is displayed.
		/// </summary>
		public DataTemplate SuggestionTemplate
		{
			get { return (DataTemplate) GetValue(SuggestionTemplateProperty); }
			set { SetValue(SuggestionTemplateProperty, value); }
		}

		/// <summary>
		///     The template selector which can provide individual data templates for each suggestions.
		/// </summary>
		public DataTemplateSelector SuggestionTemplateSelector
		{
			get { return (DataTemplateSelector) GetValue(SuggestionTemplateSelectorProperty); }
			set { SetValue(SuggestionTemplateSelectorProperty, value); }
		}

		/// <summary>
		///     The selection currently selected, if any.
		/// </summary>
		public object SelectedSuggestion
		{
			get { return GetValue(SelectedSuggestionProperty); }
			set { SetValue(SelectedSuggestionProperty, value); }
		}

		/// <summary>
		///     The index of the <see cref="SelectedSuggestion" />, if any.
		/// </summary>
		public int SelectedSuggestionIndex
		{
			get { return (int) GetValue(SelectedSuggestionIndexProperty); }
			set { SetValue(SelectedSuggestionIndexProperty, value); }
		}

		/// <summary>
		///     The watermark being displayed while no text has been entered.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		/// <summary>
		///     The text displayed (likely entered by the user).
		/// </summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		/// <summary>
		///     The list of suggestions to present to the user.
		/// </summary>
		public IEnumerable<object> Suggestions
		{
			get { return (IEnumerable<object>) GetValue(SuggestionsProperty); }
			set { SetValue(SuggestionsProperty, value); }
		}

		internal Popup Popup { get; private set; }
		internal TextBox TextBox { get; private set; }

		private void OnKeyUp()
		{
			if (Suggestions?.Count() > 0)
			{
				var previous = SelectedSuggestionIndex - 1;
				if (previous < 0)
					previous = Suggestions.Count() - 1;
				SelectedSuggestionIndex = previous;
			}
		}

		private void OnKeyDown()
		{
			if (Suggestions?.Count() > 0)
			{
				var next = (SelectedSuggestionIndex + 1) % Suggestions.Count();
				SelectedSuggestionIndex = next;
			}
		}

		private void OnEnter()
		{
			var selected = SelectedSuggestion;
			if (Suggestions?.Count() > 0 && selected != null)
			{
				var fn = SuggestionChosenCommand;
				if (fn != null && fn.CanExecute(selected))
					fn.Execute(selected);

				Popup.IsOpen = false;
				Keyboard.ClearFocus();
			}
		}

		private static void OnSuggestionsChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((SuggestionInputControl) dependencyObject).OnSuggestionsChanged((IEnumerable<object>)args.OldValue, (IEnumerable<object>) args.NewValue);
		}

		private void OnSuggestionsChanged(IEnumerable<object> oldSuggestions, IEnumerable<object> newSuggestions)
		{
			Unsubscribe(oldSuggestions);
			Subscribe(newSuggestions);
			UpdatePopup();
		}

		private void UpdatePopup()
		{
			if (Popup != null)
			{
				if (Suggestions != null && Suggestions.Any())
					Popup.IsOpen = true;
				else
					Popup.IsOpen = false;
			}
		}

		private void Subscribe(IEnumerable<object> suggestions)
		{
			var changed = suggestions as INotifyCollectionChanged;
			if (changed != null)
				changed.CollectionChanged += ChangedOnCollectionChanged;
		}

		private void Unsubscribe(IEnumerable<object> suggestions)
		{
			var changed = suggestions as INotifyCollectionChanged;
			if (changed != null)
				changed.CollectionChanged -= ChangedOnCollectionChanged;
		}

		private void ChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
		{
			UpdatePopup();
		}

		/// <inheritdoc />
		public override void OnApplyTemplate()
		{
			Popup = (Popup) GetTemplateChild(PART_SuggestionPopup);
			TextBox = (TextBox) GetTemplateChild(PART_SuggestionTextBox);
			TextBox.InputBindings.Add(_upBinding);
			TextBox.InputBindings.Add(_downBinding);

			base.OnApplyTemplate();
		}
	}
}