using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A text-box meant to input a search term.
	///     - offers a dedicated button to perform the search
	///     - offers a dedicated button to clear the search term
	///     - offers a display of the number of matches
	///     - offers buttons and shortcuts to advance to the next/previous location
	/// </summary>
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

		private static readonly DependencyPropertyKey PreviousOccurenceCommandPropertyKey =
			DependencyProperty.RegisterReadOnly("PreviousOccurenceCommand", typeof(ICommand), typeof(SearchTextBox),
												new PropertyMetadata(default(ICommand)));

		/// <summary>
		///     Definition of the <see cref="PreviousOccurenceCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty PreviousOccurenceCommandProperty = PreviousOccurenceCommandPropertyKey.DependencyProperty;

		private static readonly DependencyPropertyKey NextOccurenceCommandPropertyKey =
			DependencyProperty.RegisterReadOnly("NextOccurenceCommand", typeof(ICommand), typeof(SearchTextBox),
										new PropertyMetadata(default(ICommand)));

		/// <summary>
		///     Definition of the <see cref="NextOccurenceCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty NextOccurenceCommandProperty = NextOccurenceCommandPropertyKey.DependencyProperty;

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

		private static readonly DependencyPropertyKey BeginStartSearchCommandPropertyKey
			= DependencyProperty.RegisterReadOnly("BeginStartSearchCommand", typeof(ICommand), typeof(SearchTextBox),
			                                      new FrameworkPropertyMetadata(default(ICommand),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="BeginStartSearchCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty BeginStartSearchCommandProperty
			= BeginStartSearchCommandPropertyKey.DependencyProperty;

		private static readonly DependencyPropertyKey BeginStopSearchCommandPropertyKey
			= DependencyProperty.RegisterReadOnly("BeginStopSearchCommand", typeof (ICommand), typeof (SearchTextBox),
			                                      new FrameworkPropertyMetadata(default(ICommand),
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="BeginStopSearchCommand" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty BeginStopSearchCommandProperty
			= BeginStopSearchCommandPropertyKey.DependencyProperty;

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
			BeginStartSearchCommand = new DelegateCommand(StartSearch);
			BeginStopSearchCommand = new DelegateCommand(StopSearch);
			PreviousOccurenceCommand = new DelegateCommand(GotToPreviousOccurence);
			NextOccurenceCommand = new DelegateCommand(GotToNextOccurence);
			TextChanged += OnTextChanged;

			CommandBindings.Add(new CommandBinding(new RoutedCommand { InputGestures = { new KeyGesture(Key.F3) } }, GotToNextOccurence));
			CommandBindings.Add(new CommandBinding(new RoutedCommand { InputGestures = { new KeyGesture(Key.F3, ModifierKeys.Shift) } }, GotToPreviousOccurence));
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
		///     When <see cref="PreviousOccurenceCommand" /> or <see cref="NextOccurenceCommand" /> are executed,
		///     it is expected that this value is set to the next/previous value.
		/// </summary>
		public int CurrentOccurenceIndex
		{
			get { return (int) GetValue(CurrentOccurenceIndexProperty); }
			set { SetValue(CurrentOccurenceIndexProperty, value); }
		}

		/// <summary>
		///     The command that is executed when the user wants to jump to the next occurence
		///     of the search term in the data set.
		/// </summary>
		public ICommand NextOccurenceCommand
		{
			get { return (ICommand) GetValue(NextOccurenceCommandProperty); }
			private set { SetValue(NextOccurenceCommandPropertyKey, value); }
		}

		/// <summary>
		///     The command that is executed when the user wants to jump to the previous occurence
		///     of the search term in the data set.
		/// </summary>
		public ICommand PreviousOccurenceCommand
		{
			get { return (ICommand) GetValue(PreviousOccurenceCommandProperty); }
			private set { SetValue(PreviousOccurenceCommandPropertyKey, value); }
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
		///     The command that is executed when the user hits enter or presses the search button.
		///     Executes the <see cref="StartSearchCommand" /> internally.
		/// </summary>
		public ICommand BeginStartSearchCommand
		{
			get { return (ICommand) GetValue(BeginStartSearchCommandProperty); }
			protected set { SetValue(BeginStartSearchCommandPropertyKey, value); }
		}

		/// <summary>
		///     The command that is executed when the user hits escape or presses the remove button.
		///     Executes the <see cref="StopSearchCommand" /> internally.
		/// </summary>
		public ICommand BeginStopSearchCommand
		{
			get { return (ICommand) GetValue(BeginStopSearchCommandProperty); }
			protected set { SetValue(BeginStopSearchCommandPropertyKey, value); }
		}

		/// <summary>
		///     The watermark that is displayed until <see cref="TextBox.Text" /> becomes non-empty.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				StopSearch();
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

			ICommand command = StartSearchCommand;
			if (command != null && command.CanExecute(Text))
			{
				command.Execute(Text);
			}
		}

		private void StopSearch()
		{
			StopSearch(stealFocus: true);
		}

		private void StopSearch(bool stealFocus)
		{
			if (!IsPerformingSearch)
			{
				Text = null;
				return;
			}

			if (stealFocus)
			{
				DependencyObject scope = FocusManager.GetFocusScope(this);
				FocusManager.SetFocusedElement(scope, null);
				Application.Current.MainWindow.Focus();
			}

			ICommand command = StopSearchCommand;
			if (command != null && command.CanExecute(null))
			{
				command.Execute(null);
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
		}
	}
}