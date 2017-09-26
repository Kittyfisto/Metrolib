using System.Diagnostics.Contracts;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

// ReSharper disable CheckNamespace
namespace Metrolib.Controls
// ReSharper restore CheckNamespace
{
	/// <summary>
	///     A textbox meant to edit text.
	///     Displays a watermark while no text has been entered.
	/// </summary>
	public sealed class EditorTextBox
		: TextBox
	{
		/// <summary>
		///     Definition of the <see cref="Watermark" /> dependency property.
		/// </summary>
		public static readonly DependencyProperty WatermarkProperty =
			DependencyProperty.Register("Watermark", typeof(string), typeof(EditorTextBox),
				new PropertyMetadata(default(string)));

		static EditorTextBox()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(EditorTextBox),
				new FrameworkPropertyMetadata(typeof(EditorTextBox)));
		}

		/// <summary>
		///     Initializes this text box.
		/// </summary>
		public EditorTextBox()
		{
			InputBindings.Add(new KeyBinding(new DelegateCommand(ToggleItalic), new KeyGesture(Key.I, ModifierKeys.Control)));
			InputBindings.Add(new KeyBinding(new DelegateCommand(ToggleBoldness), new KeyGesture(Key.B, ModifierKeys.Control)));
		}

		/// <summary>
		///     The watermark text that shall appear until <see cref="TextBox.Text" /> is no longer empty.
		/// </summary>
		public string Watermark
		{
			get { return (string) GetValue(WatermarkProperty); }
			set { SetValue(WatermarkProperty, value); }
		}

		private void ToggleItalic()
		{
			if (SelectionLength > 0)
			{
				var selectionStart = SelectionStart;
				var selectionEnd = SelectionStart + SelectionLength;
				bool wasBold = IsSelectionItalic();

				var builder = new StringBuilder(Text);
				if (wasBold)
				{
					builder.Remove(selectionStart, 1);
					builder.Remove(selectionEnd - 2, 1);
				}
				else
				{
					builder.Insert(selectionStart, '*');
					builder.Insert(selectionEnd + 1, '*');
				}
				Text = builder.ToString();
				if (wasBold)
				{
					Select(selectionStart, selectionEnd - 2);
				}
				else
				{
					Select(selectionStart, selectionEnd + 2);
				}
			}
		}

		[Pure]
		private bool IsSelectionItalic()
		{
			var selectionLength = SelectionLength;

			if (selectionLength < 2)
				return false;

			var selectedText = SelectedText;
			if (selectedText[0] == '*' &&
			    selectedText[selectionLength - 1] == '*')
			{
				return true;
			}
			if (selectedText[0] == '_' &&
			    selectedText[selectionLength - 1] == '_')
			{
				return true;
			}
			return false;
		}

		private void ToggleBoldness()
		{
			if (SelectionLength > 0)
			{
				var selectionStart = SelectionStart;
				var selectionEnd = SelectionStart + SelectionLength;
				bool wasBold = IsSelectionBold();

				var builder = new StringBuilder(Text);
				if (wasBold)
				{
					builder.Remove(selectionStart, 2);
					builder.Remove(selectionEnd - 4, 2);
				}
				else
				{
					builder.Insert(selectionStart, '*');
					builder.Insert(selectionStart, '*');
					builder.Insert(selectionEnd + 2, '*');
					builder.Insert(selectionEnd + 2, '*');
				}
				Text = builder.ToString();
				if (wasBold)
				{
					Select(selectionStart, selectionEnd - 4);
				}
				else
				{
					Select(selectionStart, selectionEnd + 4);
				}
			}
		}

		[Pure]
		private bool IsSelectionBold()
		{
			var selectionLength = SelectionLength;

			if (selectionLength < 4)
				return false;

			var selectedText = SelectedText;
			if (selectedText[0] == '*' &&
			    selectedText[1] == '*' &&
			    selectedText[selectionLength - 1] == '*' &&
			    selectedText[selectionLength - 2] == '*')
			{
				return true;
			}
			if (selectedText[0] == '_' &&
			    selectedText[1] == '_' &&
			    selectedText[selectionLength - 1] == '_' &&
			    selectedText[selectionLength - 2] == '_')
			{
				return true;
			}
			return false;
		}
	}
}