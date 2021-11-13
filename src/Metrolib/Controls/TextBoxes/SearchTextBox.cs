using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A text-box meant to input a search term.
	///     * offers a dedicated button to perform the search
	///     * offers a dedicated button to clear the search term
	///     * offers a display of the number of matches
	///     * offers buttons and shortcuts to advance to the next/previous location
	/// </summary>
	[TemplatePart(Name = "PART_MovePrevious", Type = typeof (Button))]
	[TemplatePart(Name = "PART_MoveNext", Type = typeof (Button))]
	[TemplatePart(Name = "PART_RemoveText", Type = typeof (Button))]
	[TemplatePart(Name = "PART_StartSearch", Type = typeof (Button))]
	public class SearchTextBox
		: TextBox
	{
		/// <summary>
		///     Definition of the <see cref="Watermark" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty =
			DependencyProperty.Register("Watermark", typeof (string), typeof (SearchTextBox),
			                            new PropertyMetadata("Enter search term"));

		/// <summary>
		///     Definition of the <see cref="IsPerformingSearch" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsPerformingSearchProperty =
			DependencyProperty.Register("IsPerformingSearch", typeof (bool), typeof (SearchTextBox),
			                            new PropertyMetadata(default(bool)));

		/// <summary>
		///     Definition of the <see cref="StartSearchCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty StartSearchCommandProperty =
			DependencyProperty.Register("StartSearchCommand", typeof (ICommand), typeof (SearchTextBox),
			                            new PropertyMetadata(default(ICommand)));

		/// <summary>
		///     Definition of the <see cref="StopSearchCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty StopSearchCommandProperty =
			DependencyProperty.Register("StopSearchCommand", typeof (ICommand), typeof (SearchTextBox),
			                            new PropertyMetadata(default(ICommand)));

		/// <summary>
		///     Definition of the <see cref="CurrentOccurenceIndex" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty CurrentOccurenceIndexProperty =
			DependencyProperty.Register("CurrentOccurenceIndex", typeof (int), typeof (SearchTextBox),
			                            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

		/// <summary>
		///     Definition of the <see cref="OccurenceCount" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty OccurenceCountProperty =
			DependencyProperty.Register("OccurenceCount", typeof (int), typeof (SearchTextBox),
			                            new PropertyMetadata(0, OnOccurenceCountChanged));

		/// <summary>
		///     Definition of the <see cref="RequiresExplicitSearchStart" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty RequiresExplicitSearchStartProperty =
			DependencyProperty.Register("RequiresExplicitSearchStart", typeof (bool), typeof (SearchTextBox),
			                            new PropertyMetadata(true));

		private Button _moveNext;
		private Button _movePrevious;
		private Button _removeText;
		private Button _startSearch;

		static SearchTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (SearchTextBox),
			                                         new FrameworkPropertyMetadata(typeof (SearchTextBox)));
		}

		/// <summary>
		///     Initializes this SearchTextBox.
		/// </summary>
		public SearchTextBox()
		{
			TextChanged += OnTextChanged;

			CommandBindings.Add(new CommandBinding(new RoutedCommand {InputGestures = {new KeyGesture(Key.F3)}},
			                                       GotToNextOccurence));
			CommandBindings.Add(
				new CommandBinding(new RoutedCommand {InputGestures = {new KeyGesture(Key.F3, ModifierKeys.Shift)}},
				                   GotToPreviousOccurence));
		}

		/// <summary>
		///     Whether or not the search must be started by the user explicitly by pressing enter or clicking the search button.
		/// </summary>
		/// <remarks>
		///     When set to true, then the <see cref="StartSearchCommand" /> is executed when the user initiates the search.
		///     When set to false then the <see cref="StartSearchCommand" /> and <see cref="StopSearchCommand" /> are never executed
		///     and instead the search should be started once <see cref="TextBox.Text" /> is set to a non-empty value.
		///     Is set to true by default.
		/// </remarks>
		public bool RequiresExplicitSearchStart
		{
			get { return (bool) GetValue(RequiresExplicitSearchStartProperty); }
			set { SetValue(RequiresExplicitSearchStartProperty, value); }
		}

		/// <summary>
		///     Is set to true if the search started, but not (yet) stopped.
		/// </summary>
		public bool IsPerformingSearch
		{
			get { return (bool) GetValue(IsPerformingSearchProperty); }
			set { SetValue(IsPerformingSearchProperty, value); }
		}

		/// <summary>
		///     The number of occurences of the search term in the data set.
		///     Must be supplied by the user of this class.
		/// </summary>
		public int OccurenceCount
		{
			get { return (int) GetValue(OccurenceCountProperty); }
			set { SetValue(OccurenceCountProperty, value); }
		}

		/// <summary>
		///     The index of the currently focused occurence of the search term in the data set.
		/// </summary>
		public int CurrentOccurenceIndex
		{
			get { return (int) GetValue(CurrentOccurenceIndexProperty); }
			set { SetValue(CurrentOccurenceIndexProperty, value); }
		}

		/// <summary>
		///     The command that is executed when the user hits enter or presses the search button.
		/// </summary>
		public ICommand StartSearchCommand
		{
			get { return (ICommand) GetValue(StartSearchCommandProperty); }
			set { SetValue(StartSearchCommandProperty, value); }
		}

		/// <summary>
		///     The command that is executed when the user wants to stop/abort the search.
		/// </summary>
		public ICommand StopSearchCommand
		{
			get { return (ICommand) GetValue(StopSearchCommandProperty); }
			set { SetValue(StopSearchCommandProperty, value); }
		}

		/// <summary>
		///     The watermark that is displayed until <see cref="TextBox.Text" /> becomes non-empty.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		/// <summary>
		///     Is called when a control template is applied.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_movePrevious = (Button) GetTemplateChild("PART_MovePrevious");
			if (_movePrevious != null)
				_movePrevious.Command = new DelegateCommand(GotToPreviousOccurence);

			_moveNext = (Button) GetTemplateChild("PART_MoveNext");
			if (_moveNext != null)
				_moveNext.Command = new DelegateCommand(GotToNextOccurence);

			_removeText = (Button) GetTemplateChild("PART_RemoveText");
			if (_removeText != null)
				_removeText.Command = new DelegateCommand(StopSearchFromButton);

			_startSearch = (Button) GetTemplateChild("PART_StartSearch");
			if (_startSearch != null)
				_startSearch.Command = new DelegateCommand(StartSearch);
		}

		private static void OnOccurenceCountChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((SearchTextBox) dependencyObject).OnOccurenceCountChanged((int) args.NewValue);
		}

		private void OnOccurenceCountChanged(int occurenceCount)
		{
			if (occurenceCount <= 0)
			{
				CurrentOccurenceIndex = 0;
			}
		}

		/// <summary>
		///     Invoked whenever an unhandled System.Windows.Input.Keyboard.KeyDown attached routed event reaches
		///     this SearchTextBox.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				StopSearch(stealFocus: true);
				e.Handled = true;
			}
			else if (e.Key == Key.Enter)
			{
				if (IsPerformingSearch)
				{
					GotToNextOccurence();
				}
				else
				{
					StartSearch();
				}

				StartSearch();
				e.Handled = true;
			}
			else
			{
				base.OnKeyDown(e);
			}
		}

		private void GotToNextOccurence(object sender, ExecutedRoutedEventArgs e)
		{
			GotToNextOccurence();
		}

		private void GotToPreviousOccurence(object sender, ExecutedRoutedEventArgs e)
		{
			GotToPreviousOccurence();
		}

		private void GotToNextOccurence()
		{
			if (IsPerformingSearch && OccurenceCount > 0)
			{
				CurrentOccurenceIndex = (CurrentOccurenceIndex + 1)%OccurenceCount;
			}
		}

		private void GotToPreviousOccurence()
		{
			if (IsPerformingSearch && OccurenceCount > 0)
			{
				if (CurrentOccurenceIndex <= 0)
					CurrentOccurenceIndex = OccurenceCount - 1;
				else
					CurrentOccurenceIndex = CurrentOccurenceIndex - 1;
			}
		}

		private void StartSearch()
		{
			IsPerformingSearch = true;

			if (RequiresExplicitSearchStart)
			{
				ICommand command = StartSearchCommand;
				if (command != null && command.CanExecute(Text))
				{
					command.Execute(Text);
				}
			}
		}

		private void StopSearchFromButton()
		{
			StopSearch(stealFocus: false);
		}

		private void StopSearch(bool stealFocus)
		{
			if (stealFocus)
			{
				DependencyObject scope = FocusManager.GetFocusScope(this);
				FocusManager.SetFocusedElement(scope, null);
				Application.Current.MainWindow.Focus();
			}

			if (IsPerformingSearch)
			{
				if (RequiresExplicitSearchStart)
				{
					ICommand command = StopSearchCommand;
					if (command != null && command.CanExecute(null))
					{
						command.Execute(null);
					}
				}
			}

			IsPerformingSearch = false;
			Text = null;
		}

		private void OnTextChanged(object sender, TextChangedEventArgs args)
		{
			if (string.IsNullOrEmpty(Text))
			{
				StopSearch(stealFocus: false);
			}
			else if (!RequiresExplicitSearchStart)
			{
				StartSearch();
			}
		}
	}
}