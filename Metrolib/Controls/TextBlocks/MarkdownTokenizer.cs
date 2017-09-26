using System.Collections.Generic;
using System.Text;

namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	///     Responsible for splitting a markdown document into a list of tokens.
	/// </summary>
	public sealed class MarkdownTokenizer
	{
		private static readonly IReadOnlyDictionary<string, MarkdownTokenType> Tokens;

		static MarkdownTokenizer()
		{
			Tokens = new Dictionary<string, MarkdownTokenType>
			{
				// Unfortunately, our tokenizer cannot differentiate between
				// bold (**) and two italic tokens (**) and therefore we have
				// to model each character as is => It's the parsers job to
				// make sense of this duplicity
				{"*", MarkdownTokenType.Star},
				{"_", MarkdownTokenType.Underscore},

				{"~", MarkdownTokenType.Tilde},

				// Line breaks are only ever acknowledged if they are preceeded by at least two spaces
				{"  \r\n", MarkdownTokenType.LineBreak},
				{"  \n", MarkdownTokenType.LineBreak},
				// If there's no preceeding spaces, then line breaks are replaced with a single space
				{"\r\n", MarkdownTokenType.Whitespace},
				{"\n", MarkdownTokenType.Whitespace},

				{"\t", MarkdownTokenType.Whitespace},
				{" ", MarkdownTokenType.Whitespace}
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

		/// <summary>
		///     Returns an list of tokens which is semantically identical to the given list,
		///     but contains fewer tokens, if possible.
		/// </summary>
		/// <remarks>
		/// None tokens will be removed and adjacent text tokens will be merged.
		/// </remarks>
		/// <param name="tokens"></param>
		public IReadOnlyList<MarkdownToken> Optimize(IReadOnlyList<MarkdownToken> tokens)
		{
			var ret = new List<MarkdownToken>(tokens.Count);
			// #1: Remove none tokens
			foreach (var token in tokens)
				if (token.Type != MarkdownTokenType.None)
					ret.Add(token);

			// #2: Remove unnecessary space tokens
			for (var i = 0; i < ret.Count;)
			{
				if (ret[i].Type == MarkdownTokenType.Whitespace)
				{
					int n;
					for (n = i + 1; n < ret.Count; ++n)
					{
						if (ret[n].Type != MarkdownTokenType.Whitespace)
							break;
					}
					int count = n - i;
					if (count > 1)
					{
						ret.RemoveRange(i, count);
						ret.Insert(i, new MarkdownToken(MarkdownTokenType.Whitespace));
					}
				}

				++i;
			}

			// #2: Merge adjacent text/space tokens
			for (var i = 0; i < ret.Count;)
			{
				if (ret[i].Type == MarkdownTokenType.Text ||
					ret[i].Type == MarkdownTokenType.Whitespace)
				{
					int n;
					for (n = i + 1; n < ret.Count; ++n)
					{
						if (ret[n].Type != MarkdownTokenType.Text &&
							ret[n].Type != MarkdownTokenType.Whitespace)
							break;
					}
					int count = n - i;
					if (count > 1)
					{
						var token = MergeAsText(ret.Slice(i, count));
						ret.RemoveRange(i, count);
						ret.Insert(i, token);
					}
				}

				++i;
			}

			return ret;
		}

		private MarkdownToken MergeAsText(IReadOnlyList<MarkdownToken> tokens)
		{
			var builder = new StringBuilder();
			for(int i = 0; i < tokens.Count; ++i)
			{
				builder.Append(tokens[i].Type == MarkdownTokenType.Whitespace ? " " : tokens[i].Text);
			}
			return new MarkdownToken(MarkdownTokenType.Text, builder.ToString());
		}

		private static MarkdownToken? CreateNextToken(string markdown, int startIndex, out int idx)
		{
			if (startIndex >= markdown.Length)
			{
				idx = -1;
				return null;
			}

			foreach (var pair in Tokens)
				if (StartsWith(markdown, pair.Key, startIndex))
				{
					idx = startIndex + pair.Key.Length;
					return new MarkdownToken(pair.Value);
				}

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

		private static int FirstIndexOfAny(string markdown, IReadOnlyDictionary<string, MarkdownTokenType> values,
			int startIndex, out MarkdownTokenType tokenType)
		{
			for (var i = startIndex; i < markdown.Length; ++i)
				foreach (var pair in values)
					if (StartsWith(markdown, pair.Key, i))
					{
						tokenType = pair.Value;
						return i;
					}

			tokenType = MarkdownTokenType.None;
			return -1;
		}

		private static bool StartsWith(string markdown, string pattern, int startIndex)
		{
			var left = markdown.Length - startIndex;
			if (left < pattern.Length)
				return false;

			for (var i = 0; i < pattern.Length; ++i)
			{
				var expected = pattern[i];
				var actual = markdown[startIndex + i];
				if (expected != actual)
					return false;
			}

			return true;
		}
	}
}