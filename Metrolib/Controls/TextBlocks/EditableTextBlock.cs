using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     A control which behaves like a text block, until it's set to be editable.
	///     When editable, the textblock is replaced by a textbox, which allows the user to
	///     change the text property.
	/// </summary>
	[TemplatePart(Name = PART_MarkdownPresenter, Type = typeof(MarkdownBlock))]
	[TemplatePart(Name = PART_TextBox, Type = typeof(TextBox))]
	public sealed class EditableTextBlock
		: Control
	{
		/// <summary>
		///     Name of the <see cref="TextBlock" /> control.
		/// </summary>
		public const string PART_MarkdownPresenter = "PART_MarkdownPresenter";

		/// <summary>
		///     Text of the <see cref="TextBox" /> control.
		/// </summary>
		public const string PART_TextBox = "PART_TextBox";

		/// <summary>
		///     Definition of the <see cref="IsEditing" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty IsEditingProperty = DependencyProperty.Register(
			"IsEditing", typeof(bool), typeof(EditableTextBlock),
			new PropertyMetadata(defaultValue: false, propertyChangedCallback: OnIsEditingChanged));

		/// <summary>
		///     Definition of the <see cref="Text" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
			"Text", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(default(string)));

		/// <summary>
		///     Definition of the <see cref="Watermark" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(
			"Watermark", typeof(string), typeof(EditableTextBlock), new PropertyMetadata(default(string)));

		private MarkdownBlock _block;

		private TextBox _textBox;

		static EditableTextBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBlock),
				new FrameworkPropertyMetadata(typeof(EditableTextBlock)));
		}

		/// <summary>
		///     The watermark displayed in the <see cref="TextBox" /> if no text has been entered.
		///     By default, no watermark is displayed.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		/// <summary>
		///     Whether or not the text of this control is being edited.
		/// </summary>
		/// <remarks>
		///     The user can enter this state by double clicking / tapping this control.
		///     The user can leave this state by removing focus from this control (for example by pressing escape/tab).
		/// </remarks>
		/// <remarks>
		///     Setting this value to false is interpreted as ending editing mode (and NOT canceling it).
		///     If it is desired to programmatically cancel editing, then <see cref="CancelEditing" />
		///     shall be called.
		/// </remarks>
		public bool IsEditing
		{
			get { return (bool) GetValue(IsEditingProperty); }
			set { SetValue(IsEditingProperty, value); }
		}

		/// <summary>
		///     The text being displayed by this control.
		///     The user may change this text, however the newly entered value is only
		///     forwarded to this property when editing has finished, i.e. when:
		///     - The user pressed enter
		///     - The user focused another element
		///     If the user pressed escape, then editing will be cancelled and the <see cref="Text" />
		///     value will remain unchanged.
		/// </summary>
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		private static void OnIsEditingChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((EditableTextBlock) dependencyObject).OnIsEditingChanged((bool) args.NewValue);
		}

		private void OnIsEditingChanged(bool isEditing)
		{
			if (isEditing)
			{
				if (_textBox != null)
					_textBox.Text = Text;

				Dispatcher.BeginInvoke(DispatcherPriority.Background,
					new Action(() =>
					{
						if (_textBox != null)
						{
							_textBox.Focus();
							var text = _textBox.Text;
							if (text != null)
								_textBox.Select(start: 0, length: text.Length);
						}
					}));
			}
			else
			{
				Text = _textBox?.Text;
			}
		}

		/// <inheritdoc />
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			if (_block != null)
				_block.MouseLeftButtonDown -= BlockOnMouseLeftButtonDown;

			if (_textBox != null)
			{
				_textBox.LostFocus -= TextBoxOnLostFocus;
				_textBox.KeyDown -= TextBoxOnKeyDown;
			}

			_block = (MarkdownBlock) GetTemplateChild(PART_MarkdownPresenter);
			if (_block != null)
			{
				_block.MouseLeftButtonDown += BlockOnMouseLeftButtonDown;
			}

			_textBox = (TextBox) GetTemplateChild(PART_TextBox);
			if (_textBox != null)
			{
				_textBox.LostFocus += TextBoxOnLostFocus;
				_textBox.KeyDown += TextBoxOnKeyDown;
			}
		}

		private void BlockOnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
		{
			if (mouseButtonEventArgs.ClickCount >= 2)
				IsEditing = true;
		}

		private void TextBoxOnKeyDown(object sender, KeyEventArgs keyEventArgs)
		{
			switch (keyEventArgs.Key)
			{
				case Key.Escape:
					CancelEditing();
					break;

				case Key.Enter:
					IsEditing = false;
					break;
			}
		}

		private void TextBoxOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
		{
			CancelEditing();
		}

		/// <summary>
		///     Hides the <see cref="TextBox" />, undoes all changes (if any) that were performed
		///     since editing was enabled and displays the <see cref="TextBlock" /> again.
		/// </summary>
		public void CancelEditing()
		{
			var textBox = _textBox;
			if (textBox != null)
				textBox.Text = Text;

			IsEditing = false;
		}
	}
}