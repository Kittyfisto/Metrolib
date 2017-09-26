namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	/// 
	/// </summary>
	public struct MarkdownToken
	{
		public readonly MarkdownTokenType Type;
		public readonly string Text;

		public MarkdownToken(MarkdownTokenType type, string text = null)
		{
			Type = type;
			Text = text;
		}

		/// <inheritdoc />
		public override string ToString()
		{
			switch (Type)
			{
				case MarkdownTokenType.None:
					return "None";

				case MarkdownTokenType.Star:
					return "*";

				case MarkdownTokenType.Underscore:
					return "_";

				default:
					return Text;
			}
		}
	}
}