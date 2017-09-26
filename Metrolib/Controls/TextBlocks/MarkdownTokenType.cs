namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	/// 
	/// </summary>
	public enum MarkdownTokenType : byte
	{
		None = 0,

		Star,
		Underscore,
		LineBreak,
		Whitespace,
		Tilde,

		Text = 255
	}
}