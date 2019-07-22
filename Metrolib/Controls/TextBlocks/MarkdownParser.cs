using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	///     Interprets a <see cref="string" /> as markdown (<see href="https://stackoverflow.com/editing-help" />)
	///     and creates a <see cref="TextElement" /> that represents the document.
	/// </summary>
	public sealed class MarkdownParser
	{
		private readonly MarkdownTokenizer _tokenizer;

		/// <summary>
		/// </summary>
		public MarkdownParser()
		{
			_tokenizer = new MarkdownTokenizer();
		}

		/// <summary>
		///     Parses the given markdown document and creates a document tree consisting of
		///     <see cref="TextElement" />s.
		/// </summary>
		/// <param name="markdown"></param>
		/// <returns></returns>
		public IReadOnlyList<Inline> Parse(string markdown)
		{
			var tokens = _tokenizer.Optimize(_tokenizer.Tokenize(markdown));
			return MatchAll(tokens);
		}

		private IReadOnlyList<Inline> MatchAll(IReadOnlyList<MarkdownToken> tokens)
		{
			var ret = new List<Inline>();

			int totalConsumed = 0;
			IReadOnlyList<MarkdownToken> remainingTokens = tokens;
			while (remainingTokens.Count > 0)
			{
				Inline element;
				int consumed;
				if ((element = MatchOne(remainingTokens, out consumed)) != null)
				{
					ret.Add(element);
					totalConsumed += consumed;
					remainingTokens = tokens.Slice(totalConsumed);
				}
				else
				{
					break;
				}
			}

			return ret;
		}

		private string ToString(IReadOnlyList<MarkdownToken> tokens)
		{
			var builder = new StringBuilder();
			foreach (var token in tokens)
			{
				builder.Append(token.Text);
			}

			return builder.ToString();
		}

		private Inline MatchOne(IReadOnlyList<MarkdownToken> tokens, out int consumedCount)
		{
			int totalMatchCount;
			IReadOnlyList<MarkdownToken> match;
			if ((match = MatchOneStar(tokens, out totalMatchCount,
				    TokenPattern.Required(MarkdownTokenType.Star, MarkdownTokenType.Star),
				    TokenPattern.Optional(MarkdownTokenType.Star, MarkdownTokenType.Star))) != null)
			{
				var children = MatchAll(match);
				var element = new Bold();
				element.Inlines.AddRange(children);
				consumedCount = 4 + match.Count;
				return element;
			}

			if ((match = MatchOneStar(tokens, out totalMatchCount,
				    TokenPattern.Required(MarkdownTokenType.Underscore, MarkdownTokenType.Underscore),
				    TokenPattern.Optional(MarkdownTokenType.Underscore, MarkdownTokenType.Underscore))) != null)
			{
				var children = MatchAll(match);
				var element = new Bold();
				element.Inlines.AddRange(children);
				consumedCount = 4 + match.Count;
				return element;
			}

			if ((match = MatchOneStar(tokens, out totalMatchCount,
				    TokenPattern.Required(MarkdownTokenType.Tilde, MarkdownTokenType.Tilde),
				    TokenPattern.Optional(MarkdownTokenType.Tilde, MarkdownTokenType.Tilde))) != null)
			{
				var children = MatchAll(match);
				var element = new Span();
				var decoration = new TextDecoration {Location = TextDecorationLocation.Strikethrough};
				decoration.Freeze();
				element.TextDecorations.Add(decoration);
				element.Inlines.AddRange(children);
				consumedCount = 4 + match.Count;
				return element;
			}

			if ((match = MatchOneStar(tokens, out totalMatchCount,
				    TokenPattern.Required(MarkdownTokenType.Star),
				    TokenPattern.Optional(MarkdownTokenType.Star))) != null)
			{
				var children = MatchAll(match);
				var element = new Italic();
				element.Inlines.AddRange(children);
				consumedCount = 2 + match.Count;
				return element;
			}

			if ((match = MatchOneStar(tokens, out totalMatchCount,
				    TokenPattern.Required(MarkdownTokenType.Underscore),
				    TokenPattern.Optional(MarkdownTokenType.Underscore))) != null)
			{
				var children = MatchAll(match);
				var element = new Italic();
				element.Inlines.AddRange(children);
				consumedCount = 2 + match.Count;
				return element;
			}

			if (MatchTwoStar(tokens, out totalMatchCount,
				    start1: TokenPattern.Required(MarkdownTokenType.SquareBracketOpen),
					end1: TokenPattern.Required(MarkdownTokenType.SquareBracketClose),
					start2: TokenPattern.Required(MarkdownTokenType.BracketOpen),
				    end2: TokenPattern.Required(MarkdownTokenType.BracketClose),
					match1: out var match1,
					match2: out var match2))
			{
				consumedCount = totalMatchCount;
				var uri = new Uri(ToString(match2));
				var hyperlink = new Hyperlink()
				{
					NavigateUri = uri
				};
				hyperlink.RequestNavigate += (a, b) => HyperlinkRun.NavigateTo(uri, null, null);
				hyperlink.Inlines.AddRange(MatchAll(match1));
				return hyperlink;
			}

			if ((match = MatchOneStar(tokens, out totalMatchCount,
				start: TokenPattern.Required(MarkdownTokenType.LineBreak))) != null)
			{
				consumedCount = 1;
				return new LineBreak();
			}

			if ((match = MatchOneStar(tokens, out totalMatchCount,
				    start: TokenPattern.Required(MarkdownTokenType.Whitespace))) != null)
			{
				consumedCount = 1;
				return new Run(" ");
			}

			var token = tokens[index: 0];
			consumedCount = 1;
			return new Run(token.Text);
		}

		/// <summary>
		///     Tries to find a match with the given pattern(s).
		///     If start is specified, then the match must occur at the very first token.
		///     If end is specified, then the match includes every token until the first token
		///     matching the end pattern.
		/// </summary>
		/// <param name="tokens"></param>
		/// <param name="matchTotalTokenCount"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns>The list of matching tokens, excluding start/end tokens</returns>
		private static IReadOnlyList<MarkdownToken> MatchOneStar(IReadOnlyList<MarkdownToken> tokens,
			out int matchTotalTokenCount,
			TokenPattern? start = null, TokenPattern? end = null)
		{
			var startIndex = 0;
			if (start != null)
			{
				if (!StartsWith(tokens, start.Value))
				{
					matchTotalTokenCount = 0;
					return null;
				}

				startIndex += start.Value.Length;
			}

			if (end != null)
			{
				var slice = tokens.Slice(startIndex, tokens.Count - startIndex);
				var idx = FindFirst(slice, end.Value);
				if (idx == -1)
					if (end.Value.IsRequired)
					{
						matchTotalTokenCount = 0;
						return null;
					}
					else
					{
						// We have a match until the end
						matchTotalTokenCount = tokens.Count;
						return tokens.Slice(startIndex, tokens.Count - startIndex);
					}
				// We have a match
				matchTotalTokenCount = startIndex + idx + end.Value.Length;
				return tokens.Slice(startIndex, idx);
			}
			// We have a match
			matchTotalTokenCount = tokens.Count;
			return tokens.Slice(startIndex, tokens.Count - startIndex);
		}

		private static bool MatchTwoStar(IReadOnlyList<MarkdownToken> tokens,
			out int matchTotalTokenCount,
			TokenPattern? start1, TokenPattern? end1,
			TokenPattern? start2, TokenPattern? end2,
			out IReadOnlyList<MarkdownToken> match1,
			out IReadOnlyList<MarkdownToken> match2)
		{
			match1 = MatchOneStar(tokens, out var match1Count, start1, end1);
			if (match1 == null)
			{
				matchTotalTokenCount = 0;
				match2 = null;
				return false;
			}

			var nextTokens = tokens.Skip(match1Count).ToList();
			match2 = MatchOneStar(nextTokens, out var match2Count, start2, end2);
			if (match2 == null)
			{
				matchTotalTokenCount = 0;
				return false;
			}

			matchTotalTokenCount = match1Count + match2Count;
			return true;
		}

		private static bool StartsWith(IReadOnlyList<MarkdownToken> tokens, TokenPattern pattern)
		{
			if (tokens.Count < pattern.Length)
				return false;

			for (var i = 0; i < pattern.Length; ++i)
			{
				var token = tokens[i];
				var expected = pattern.Types[i];
				if (token.Type != expected)
					return false;
			}

			return true;
		}

		private static int FindFirst(IReadOnlyList<MarkdownToken> tokens, TokenPattern pattern)
		{
			for (var i = 0; i < tokens.Count; ++i)
				if (StartsWith(tokens.Slice(i), pattern))
					return i;

			return -1;
		}

		private struct TokenPattern
		{
			public readonly bool IsRequired;
			public readonly MarkdownTokenType[] Types;
			public int Length => Types.Length;

			public static TokenPattern Required(params MarkdownTokenType[] types)
			{
				return new TokenPattern(isRequired: true, types: types);
			}

			public static TokenPattern Optional(params MarkdownTokenType[] types)
			{
				return new TokenPattern(isRequired: false, types: types);
			}

			private TokenPattern(bool isRequired, params MarkdownTokenType[] types)
			{
				IsRequired = isRequired;
				Types = types;
			}
		}
	}
}