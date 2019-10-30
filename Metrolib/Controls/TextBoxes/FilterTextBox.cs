using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace

namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A textbox meant to input queries used for filtering.
	/// </summary>
	public class FilterTextBox : Control
	{
		/// <summary>
		///     Definition of the <see cref="FilterText" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty FilterTextProperty =
			DependencyProperty.Register("FilterText", typeof (string), typeof (FilterTextBox),
			                            new PropertyMetadata(null, OnFilterTextChanged));

		/// <summary>
		///     Definition of the <see cref="Watermark" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty =
			DependencyProperty.Register("Watermark", typeof (string), typeof (FilterTextBox),
			                            new PropertyMetadata(default(string)));

		/// <summary>
		///     Definition of the <see cref="IsValid" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsValidProperty =
			DependencyProperty.Register("IsValid", typeof (bool), typeof (FilterTextBox), new PropertyMetadata(true));

		private static readonly DependencyPropertyKey HasFilterTextPropertyKey
			= DependencyProperty.RegisterReadOnly("HasFilterText", typeof (bool), typeof (FilterTextBox),
			                                      new FrameworkPropertyMetadata(false,
			                                                                    FrameworkPropertyMetadataOptions.None));

		/// <summary>
		///     Definition of the <see cref="HasFilterText" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty HasFilterTextProperty
			= HasFilterTextPropertyKey.DependencyProperty;

		/// <summary>
		///     Definition of the <see cref="AcceptsTab"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty AcceptsTabProperty = DependencyProperty.Register(
			"AcceptsTab", typeof(bool), typeof(FilterTextBox), new PropertyMetadata(false));

		/// <summary>
		///     Definition of the <see cref="AcceptsReturn"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty AcceptsReturnProperty = DependencyProperty.Register(
			"AcceptsReturn", typeof(bool), typeof(FilterTextBox), new PropertyMetadata(false));

		private TextBox _filterInput;
		private Button _removeFilterTextButton;

		static FilterTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof (FilterTextBox),
			                                         new FrameworkPropertyMetadata(typeof (FilterTextBox)));
		}

		/// <summary>
		///     Initializes this object.
		/// </summary>
		public FilterTextBox()
		{
			GotFocus += OnGotFocus;
		}

		/// <summary>
		///     The button that appears to remove entered text.
		/// </summary>
		/// <remarks>
		///     Is used for unit tests.
		/// </remarks>
		public Button RemoveFilterTextButton
		{
			get { return _removeFilterTextButton; }
		}

		/// <summary>
		///     Whether or not any <see cref="FilterText" /> has been entered.
		/// </summary>
		public bool HasFilterText
		{
			get { return (bool) GetValue(HasFilterTextProperty); }
			protected set { SetValue(HasFilterTextPropertyKey, value); }
		}

		/// <summary>
		///     Whether or not the entered <see cref="FilterText" /> is valid.
		/// </summary>
		/// <remarks>
		///     When it is not, then the background is colored red to let the user know.
		///     Can be used when the user has to enter text in a specific format, for example
		///     SQL or a regular expression.
		/// </remarks>
		public bool IsValid
		{
			get { return (bool) GetValue(IsValidProperty); }
			set { SetValue(IsValidProperty, value); }
		}

		/// <summary>
		///     The watermark text that shall be displayed when the user hasn't entered any <see cref="FilterText" /> (yet).
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		/// <summary>
		///     The filter text input by the user.
		/// </summary>
		public string FilterText
		{
			get { return (string) GetValue(FilterTextProperty); }
			set { SetValue(FilterTextProperty, value); }
		}

		/// <summary>
		///     Gets or sets a value indicating whether pressing the TAB key in a multiline text box control types a TAB character in the control instead of moving the focus to the next control in the tab order.
		/// </summary>
		public bool AcceptsTab
		{
			get { return (bool)GetValue(AcceptsTabProperty); }
			set { SetValue(AcceptsTabProperty, value); }
		}

		/// <summary>
		///     Gets or sets a value indicating whether pressing ENTER in a multiline TextBox control creates a new line of text in the control or activates the default button for the form.
		/// </summary>
		public bool AcceptsReturn
		{
			get { return (bool)GetValue(AcceptsReturnProperty); }
			set { SetValue(AcceptsReturnProperty, value); }
		}

		private static void OnFilterTextChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((FilterTextBox) dependencyObject).OnFilterTextChanged((string) args.NewValue);
		}

		private void OnFilterTextChanged(string filterText)
		{
			HasFilterText = !string.IsNullOrEmpty(filterText);
		}

		/// <summary>
		///     Invoked when an unhandled System.Windows.Input.Keyboard.KeyDown attached event reaches
		///     this FilterTextBox.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				DependencyObject scope = FocusManager.GetFocusScope(_filterInput);
				FocusManager.SetFocusedElement(scope, null);
				Application.Current.MainWindow.Focus();
			}
			base.OnKeyDown(e);
		}

		/// <summary>
		///     Called when the template's tree is generated.
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (_removeFilterTextButton != null)
			{
				_removeFilterTextButton.Click -= RemoveFilterTextButtonOnClick;
			}

			_filterInput = (TextBox) GetTemplateChild("PART_FilterInput");
			_removeFilterTextButton = (Button) GetTemplateChild("PART_RemoveFilterText");

			if (_removeFilterTextButton != null)
			{
				_removeFilterTextButton.Click += RemoveFilterTextButtonOnClick;
			}
		}

		private void RemoveFilterTextButtonOnClick(object sender, RoutedEventArgs routedEventArgs)
		{
			FilterText = null;
		}

		private void OnGotFocus(object sender, RoutedEventArgs routedEventArgs)
		{
			_filterInput.Focus();
		}
	}
}