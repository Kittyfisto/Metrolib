using System.Collections.Generic;

namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	///     Responsible for splitting a markdown document into a list of tokens.
	/// </summary>
	public sealed class MarkdownTokenizer
	{
		private static readonly IReadOnlyDictionary<char, MarkdownTokenType> Tokens;

		static MarkdownTokenizer()
		{
			Tokens = new Dictionary<char, MarkdownTokenType>
			{
				{'*', MarkdownTokenType.Star},
				{'_', MarkdownTokenType.Underscore}
			};
		}

		/// <summary>
		///     Parses the given markdown document and produces a list of tokens representing
		///     the document.
		/// </summary>
		/// <param name="markdown"></param>
		/// <returns></returns>
		public List<MarkdownToken> Tokenize(string markdown)
		{
			var ret = new List<MarkdownToken>();
			if (markdown != null)
			{
				int idx;
				var startIndex = 0;
				MarkdownToken? token;
				while ((token = CreateNextToken(markdown, startIndex, out idx)) != null)
				{
					ret.Add(token.Value);
					if (idx <= startIndex)
						break;

					startIndex = idx;
				}
			}
			return ret;
		}

		private static MarkdownToken? CreateNextToken(string markdown, int startIndex, out int idx)
		{
			if (startIndex >= markdown.Length)
			{
				idx = -1;
				return null;
			}

			var next = markdown[startIndex];
			switch (next)
			{
				case '*':
					idx = startIndex + 1;
					return new MarkdownToken(MarkdownTokenType.Star);

				case '_':
					idx = startIndex + 1;
					return new MarkdownToken(MarkdownTokenType.Underscore);

				default:
					MarkdownTokenType type;
					var ret = FirstIndexOfAny(markdown, Tokens, startIndex, out type);
					string sub;
					if (ret == -1)
					{
						sub = markdown.Substring(startIndex);
						idx = markdown.Length;
					}
					else
					{
						sub = markdown.Substring(startIndex, ret - startIndex);
						idx = ret;
					}
					return new MarkdownToken(MarkdownTokenType.Text, sub);
			}
		}

		private static int FirstIndexOfAny(string markdown, IReadOnlyDictionary<char, MarkdownTokenType> values,
			int startIndex, out MarkdownTokenType tokenType)
		{
			for (var i = startIndex; i < markdown.Length; ++i)
			{
				var value = markdown[i];
				foreach (var pair in values)
					if (value == pair.Key)
					{
						tokenType = pair.Value;
						return i;
					}
			}

			tokenType = MarkdownTokenType.None;
			return -1;
		}
	}
}