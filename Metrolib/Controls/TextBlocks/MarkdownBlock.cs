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
	[TemplatePart(Name = PART_TextBlock, Type = typeof(TextBlock))]
	public sealed class MarkdownBlock
		: Control
	{
		public const string PART_TextBlock = "PART_TextBlock";

		public static readonly DependencyProperty MarkdownProperty = DependencyProperty.Register(
			"Markdown", typeof(string), typeof(MarkdownBlock),
			new PropertyMetadata(defaultValue: null, propertyChangedCallback: OnMarkdownChanged));

		private TextBlock _textBlock;

		static MarkdownBlock()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownBlock),
				new FrameworkPropertyMetadata(typeof(MarkdownBlock)));
		}

		public string Markdown
		{
			get { return (string) GetValue(MarkdownProperty); }
			set { SetValue(MarkdownProperty, value); }
		}

		private static void OnMarkdownChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
		{
			((MarkdownBlock) dependencyObject).OnMarkdownChanged((string) args.NewValue);
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