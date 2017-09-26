using System;

namespace Metrolib.Controls.TextBlocks
{
	/// <summary>
	/// </summary>
	public struct MarkdownToken
		: IEquatable<MarkdownToken>
	{
		/// <summary>
		///     A "none" (empty) token. Represents text
		///     which has been stripped from the original document.
		/// </summary>
		public static readonly MarkdownToken None = new MarkdownToken(MarkdownTokenType.None);

		/// <inheritdoc />
		public bool Equals(MarkdownToken other)
		{
			return Type == other.Type && string.Equals(Text, other.Text);
		}

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(objA: null, objB: obj)) return false;
			return obj is MarkdownToken && Equals((MarkdownToken) obj);
		}

		/// <inheritdoc />
		public override int GetHashCode()
		{
			unchecked
			{
				return ((int) Type * 397) ^ (Text != null ? Text.GetHashCode() : 0);
			}
		}

		public static bool operator ==(MarkdownToken left, MarkdownToken right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(MarkdownToken left, MarkdownToken right)
		{
			return !left.Equals(right);
		}

		public readonly MarkdownTokenType Type;
		public readonly string Text;

		public MarkdownToken(string text)
		{
			Type = MarkdownTokenType.Text;
			Text = text;
		}

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

				case MarkdownTokenType.LineBreak:
					return "br";

				case MarkdownTokenType.Whitespace:
					return "space";

				default:
					return Text;
			}
		}
	}
}