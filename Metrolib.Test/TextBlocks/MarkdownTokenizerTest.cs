using FluentAssertions;
using Metrolib.Controls.TextBlocks;
using NUnit.Framework;

namespace Metrolib.Test.TextBlocks
{
	[TestFixture]
	public sealed class MarkdownTokenizerTest
	{
		[Test]
		public void TestBold1()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("**").Should().Equal(
				new MarkdownToken(MarkdownTokenType.Star),
				new MarkdownToken(MarkdownTokenType.Star)
				);
		}

		[Test]
		public void TestBold2()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("__").Should().Equal(
				new MarkdownToken(MarkdownTokenType.Underscore),
				new MarkdownToken(MarkdownTokenType.Underscore)
			);
		}

		[Test]
		public void TestItalic1()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("*").Should().Equal(new MarkdownToken(MarkdownTokenType.Star));
		}

		[Test]
		public void TestItalic2()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("_").Should().Equal(new MarkdownToken(MarkdownTokenType.Underscore));
		}

		[Test]
		public void TestLineBreak1()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("  \r\n").Should().Equal(new MarkdownToken(MarkdownTokenType.LineBreak));
		}

		[Test]
		public void TestLineBreak2()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("  \n").Should().Equal(new MarkdownToken(MarkdownTokenType.LineBreak));
		}

		[Test]
		public void TestLineBreak3()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("\r\n").Should().Equal(new MarkdownToken(MarkdownTokenType.Whitespace));
			tokenizer.Tokenize("\n").Should().Equal(new MarkdownToken(MarkdownTokenType.Whitespace));
		}

		[Test]
		public void TestLineBreak4()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("Hello,\r\nWorld!").Should().Equal(
				new MarkdownToken(MarkdownTokenType.Text, "Hello,"),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Text, "World!")
			);
		}

		[Test]
		public void TestSpace1()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize(" ").Should().Equal(new MarkdownToken(MarkdownTokenType.Whitespace));
		}

		[Test]
		public void TestSpace2()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Tokenize("\t").Should().Equal(new MarkdownToken(MarkdownTokenType.Whitespace));
		}

		[Test]
		public void TestOptimizeEmpty()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(new MarkdownToken[0]).Should().BeEmpty();
		}

		[Test]
		public void TestOptimizeOneNoneToken()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(new [] {new MarkdownToken(MarkdownTokenType.None)}).Should().BeEmpty();
		}

		[Test]
		public void TestOptimizeTwoNoneTokens()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(new[]
			{
				new MarkdownToken(MarkdownTokenType.None),
				new MarkdownToken(MarkdownTokenType.None)
			}).Should().BeEmpty();
		}

		[Test]
		public void TestOptimizeTwoTextTokens()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(new[]
			{
				new MarkdownToken("Foo"),
				new MarkdownToken("bar")
			}).Should().Equal(new MarkdownToken("Foobar"));
		}

		[Test]
		[Description("Verifies that multiple adjacent text tokens are merged into one")]
		public void TestOptimizeFiveTextTokens()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(new[]
			{
				new MarkdownToken("F"),
				new MarkdownToken("o"),
				new MarkdownToken("o"),
				new MarkdownToken("b"),
				new MarkdownToken("a"),
				new MarkdownToken("r")
			}).Should().Equal(new MarkdownToken("Foobar"));
		}

		[Test]
		[Description("Verifies that multiple adjacent space tokens are merged into one")]
		public void TestOptimizeSpaceTokens()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(new[]
			{
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace)
			}).Should().Equal(new MarkdownToken(MarkdownTokenType.Whitespace));
		}

		[Test]
		[Description("Verifies that multiple adjacent space tokens are merged into one text token")]
		public void TestOptimizeSpaceAndTextTokens()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(new[]
			{
				new MarkdownToken("Foo"),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken(MarkdownTokenType.Whitespace),
				new MarkdownToken("b"),
				new MarkdownToken("a"),
				new MarkdownToken("r")
			}).Should().Equal(
				new MarkdownToken("Foo bar")
				);
		}

		[Test]
		[Description("Verifies that untrue linebreaks are replaced with spaces")]
		public void TestTokenizeAndOptimize()
		{
			var tokenizer = new MarkdownTokenizer();
			tokenizer.Optimize(tokenizer.Tokenize("Hello\r\nWorld!"))
				.Should().Equal(new MarkdownToken("Hello World!"));
		}
	}
}