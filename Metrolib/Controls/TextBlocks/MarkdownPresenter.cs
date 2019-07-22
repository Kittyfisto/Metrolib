using System.Windows;
using System.Windows.Controls;
using Metrolib.Controls.TextBlocks;

// ReSharper disable once CheckNamespace
namespace Metrolib.Controls
{
	/// <summary>
	///     This control is responsible for presenting markdown text.
	///     <see cref="https://stackoverflow.com/editing-help" /> for a description of markdown.
	/// </summary>
	/// <remarks>
	///     Currently, this control *only* supports the following constructs:
	///     - Bold
	///     - Italic
	///     - Strikethrough
	///     - Hyperlinks
	///
	///     If there's a construct you're interested in, please open an issue and I will reprioritize
	///     accordingly.
	/// </remarks>
	[TemplatePart(Name = PART_TextBlock, Type = typeof(TextBlock))]
	public sealed class MarkdownPresenter
		: Control
	{
		public const string PART_TextBlock = "PART_TextBlock";

		/// <summary>
		///     Definition of the <see cref="Markdown"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty MarkdownProperty = DependencyProperty.Register(
			"Markdown", typeof(string), typeof(MarkdownPresenter),
			new PropertyMetadata(defaultValue: null, propertyChangedCallback: OnMarkdownChanged));

		/// <summary>
		///     Definition of the <see cref="TextWrapping"/> dependency property.
		/// </summary>
		public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
		                                                "TextWrapping", typeof(TextWrapping), typeof(MarkdownPresenter), new PropertyMetadata(TextWrapping.Wrap));

		/// <summary>
		///     Gets or sets how this control should wrap text.
		/// </summary>
		public TextWrapping TextWrapping
		{
			get { return (TextWrapping) GetValue(TextWrappingProperty); }
			set { SetValue(TextWrappingProperty, value); }
		}

		private TextBlock _textBlock;

		static MarkdownPresenter()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownPresenter),
				new FrameworkPropertyMetadata(typeof(MarkdownPresenter)));
		}

		/// <summary>
		///     Gets or sets the markdown text this control should display.
		/// </summary>
		public string Markdown
		{
			get { return (string) GetValue(MarkdownProperty); }
			set { SetValue(MarkdownProperty, value); }
		}

		private static void OnMarkdownChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((MarkdownPresenter) dependencyObject).OnMarkdownChanged((string) args.NewValue);
		}

		/// <inheritdoc />
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			_textBlock = GetTemplateChild(PART_TextBlock) as TextBlock;
			OnMarkdownChanged(Markdown);
		}

		private void OnMarkdownChanged(string markdown)
		{
			if (_textBlock != null)
			{
				var parser = new MarkdownParser();
				var element = parser.Parse(markdown);
				_textBlock.Inlines.Clear();
				_textBlock.Inlines.AddRange(element);
			}
		}
	}
}