using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	/// Interprets a <see cref="string"/> as markdown (<see cref="https://stackoverflow.com/editing-help"/>)
	/// and creates a <see cref="TextElement"/> that represents the document.
	/// </summary>
	public sealed class MarkdownParser
	{
		public IReadOnlyList<Inline> Parse(string markdown)
		{
			if (markdown == null)
				return new Inline[0];

			int unused;
			return ParseLeftHandRecursive(markdown, 0, markdown.Length, false, false, out unused);
		}

		private IReadOnlyList<Inline> ParseLeftHandRecursive(string markdown, int startIndex, int count, bool isBold, bool isItalic, out int consumedCount)
		{
			var ret = new List<Inline>();
			consumedCount = 0;

			do
			{
				if (IsBold(markdown, startIndex, count))
				{
					consumedCount += 2;
					int tmp;
					var children = ParseLeftHandRecursive(markdown, startIndex + 2, count - 2, true, isItalic, out tmp);
					consumedCount += tmp;

					var element = new Bold();
					element.Inlines.AddRange(children);
					ret.Add(element);
				}
				else if (IsItalic(markdown, startIndex, count))
				{
					consumedCount += 1;
					int tmp;
					var children = ParseLeftHandRecursive(markdown, startIndex + 1, count - 1, isBold, true, out tmp);
					consumedCount += tmp;

					var element = new Italic();
					element.Inlines.AddRange(children);
					ret.Add(element);
				}
				else if (isItalic)
				{
					// Try to consume until the next italic marker.
					// That marker will have to be interpreted as an end marker
					var index = markdown.IndexOfAny(new[] {'*', '_'}, startIndex, count);
					if (index != -1)
					{
						string substring = markdown.Substring(startIndex, index - startIndex);
						ret.Add(new Run(substring));
						consumedCount = substring.Length + 1;
					}
					else
					{
						string substring = markdown.Substring(startIndex);
						ret.Add(new Run(substring));
						consumedCount = substring.Length;
					}

					break;
				}
				else if (isBold)
				{
					// Try to consume until the next bold marker.
					// That marker will have to be interpreted as an end marker
					var index = IndexOfAny(markdown, new[] {"**", "__"}, startIndex, count);
					if (index != -1)
					{
						string substring = markdown.Substring(startIndex, index - startIndex);
						ret.Add(new Run(substring));
						consumedCount = substring.Length + 2;
					}
					else
					{
						string substring = markdown.Substring(startIndex);
						ret.Add(new Run(substring));
						consumedCount = substring.Length;
					}

					break;
				}
				else
				{
					// We will consume everything
					var substring = markdown.Substring(startIndex);
					ret.Add(new Run(substring));
					consumedCount = substring.Length;
				}

				if (consumedCount == 0)
				{
					// This is a serious parsing error - we better break
					// and display bad text instead of not returning at all...
					break;
				}

				startIndex += consumedCount;
				count -= consumedCount;
			} while (count > 0);

			return ret;
		}

		private bool IsBold(string markdown, int startIndex, int count)
		{
			if (count < 2)
				return false;

			var first = markdown[startIndex];
			var second = markdown[startIndex + 1];

			if (first == '*' && second == '*' ||
			    first == '_' && second == '_')
				return true;

			return false;
		}

		private bool IsItalic(string markdown, int startIndex, int count)
		{
			if (count < 1)
				return false;

			var first = markdown[startIndex];

			if (first == '*' || first == '_')
				return true;

			return false;
		}

		private static int IndexOfAny(string markdown, string[] values, int startIndex, int count)
		{
			for (int i = startIndex; i < startIndex + count; ++i)
			{
				int left = startIndex + count - i;
				for (int n = 0; n < values.Length; ++n)
				{
					if (StartsWith(markdown, values[n], i, left))
					{
						return i;
					}
				}
			}

			return -1;
		}

		private static bool StartsWith(string markdown, string search, int startIndex, int count)
		{
			if (count < search.Length)
				return false;

			for (int i = 0; i < search.Length; ++i)
			{
				var v = markdown[startIndex+i];
				if (v != search[i])
					return false;
			}

			return true;
		}

		private static int FirstIndexOfAny(string markdown, char[] values, int startIndex, out int valueIndex)
		{
			for (int i = startIndex; i < markdown.Length; ++i)
			{
				var value = markdown[i];
				for (int n = 0; n < values.Length; ++n)
				{
					if (value == n)
					{
						valueIndex = n;
						return i;
					}
				}
			}

			valueIndex = -1;
			return -1;
		}
	}
}