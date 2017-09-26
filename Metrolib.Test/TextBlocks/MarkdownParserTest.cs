using System.Linq;
using System.Windows.Documents;
using FluentAssertions;
using Metrolib.Controls.TextBlocks;
using NUnit.Framework;

namespace Metrolib.Test.TextBlocks
{
	[TestFixture]
	public sealed class MarkdownParserTest
	{
		[Test]
		public void TestNull()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse(null);
			values.Should().NotBeNull();
			values.Should().BeEmpty();
		}

		[Test]
		public void TestEmpty()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse(string.Empty);
			values.Should().NotBeNull();
			values.Should().BeEmpty();
		}

		[Test]
		public void TestParseLineBreak1()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("Hello,\r\nWorld!");
			values.Should().NotBeNull();
			values.Should().HaveCount(1);
			values[0].Should().BeOfType<Run>();
			((Run) values[0]).Text.Should().Be("Hello, World!");
		}

		[Test]
		public void TestParseLineBreak2()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("Hello,  \r\nWorld!");
			values.Should().NotBeNull();
			values.Should().HaveCount(3);
			values[0].Should().BeOfType<Run>();
			((Run)values[0]).Text.Should().Be("Hello,");
			values[1].Should().BeOfType<LineBreak>();
			values[2].Should().BeOfType<Run>();
			((Run)values[2]).Text.Should().Be("World!");
		}

		[Test]
		public void TestTextOnly()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("Hello, World!");
			values.Should().NotBeNull();
			values.Should().HaveCount(1);
			values[0].Should().NotBeNull();
			values[0].Should().BeOfType<Run>();
			((Run) values[0]).Text.Should().Be("Hello, World!");
		}

		[Test]
		public void TestItalic1()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("*f*");
			values.Should().HaveCount(1);
			values[0].Should().BeOfType<Italic>();
			((Italic) values[0]).Inlines.Should().HaveCount(1);
			((Italic) values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run) ((Italic) values[0]).Inlines.ElementAt(0)).Text.Should().Be("f");
		}

		[Test]
		public void TestItalic2()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("*f**o*");
			values.Should().HaveCount(2);
			values[0].Should().BeOfType<Italic>();
			((Italic)values[0]).Inlines.Should().HaveCount(1);
			((Italic)values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Italic)values[0]).Inlines.ElementAt(0)).Text.Should().Be("f");
			values[1].Should().BeOfType<Italic>();
			((Italic)values[1]).Inlines.Should().HaveCount(1);
			((Italic)values[1]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Italic)values[1]).Inlines.ElementAt(0)).Text.Should().Be("o");
		}

		[Test]
		public void TestBold1()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("**f**");
			values.Should().HaveCount(1);
			values[0].Should().BeOfType<Bold>();
			((Bold)values[0]).Inlines.Should().HaveCount(1);
			((Bold)values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[0]).Inlines.ElementAt(0)).Text.Should().Be("f");
		}

		[Test]
		public void TestBold2()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("**f****o**");
			values.Should().HaveCount(2);
			values[0].Should().BeOfType<Bold>();
			((Bold)values[0]).Inlines.Should().HaveCount(1);
			((Bold)values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[0]).Inlines.ElementAt(0)).Text.Should().Be("f");
			values[1].Should().BeOfType<Bold>();
			((Bold)values[1]).Inlines.Should().HaveCount(1);
			((Bold)values[1]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[1]).Inlines.ElementAt(0)).Text.Should().Be("o");
		}

		[Test]
		public void TestItalicBold()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("*shit***happens**");
			values.Should().HaveCount(2);
			values[0].Should().BeOfType<Italic>();
			((Italic)values[0]).Inlines.Should().HaveCount(1);
			((Italic)values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Italic)values[0]).Inlines.ElementAt(0)).Text.Should().Be("shit");
			values[1].Should().BeOfType<Bold>();
			((Bold)values[1]).Inlines.Should().HaveCount(1);
			((Bold)values[1]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[1]).Inlines.ElementAt(0)).Text.Should().Be("happens");
		}

		[Test]
		public void TestBoldItalic()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("**shit***happens*");
			values.Should().HaveCount(2);
			values[0].Should().BeOfType<Bold>();
			((Bold)values[0]).Inlines.Should().HaveCount(1);
			((Bold)values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[0]).Inlines.ElementAt(0)).Text.Should().Be("shit");
			values[1].Should().BeOfType<Italic>();
			((Italic)values[1]).Inlines.Should().HaveCount(1);
			((Italic)values[1]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Italic)values[1]).Inlines.ElementAt(0)).Text.Should().Be("happens");
		}

		[Test]
		public void TestBoldNormal()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("**shit**happens");
			values.Should().HaveCount(2);
			values[0].Should().BeOfType<Bold>();
			((Bold)values[0]).Inlines.Should().HaveCount(1);
			((Bold)values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[0]).Inlines.ElementAt(0)).Text.Should().Be("shit");
			values[1].Should().BeOfType<Run>();
			((Run)values[1]).Text.Should().Be("happens");
		}

		[Test]
		public void TestBoldItalicNormal()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("**f***o*o");
			values.Should().HaveCount(3);
			values[0].Should().BeOfType<Bold>();
			((Bold)values[0]).Inlines.Should().HaveCount(1);
			((Bold)values[0]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[0]).Inlines.ElementAt(0)).Text.Should().Be("f");
			values[1].Should().BeOfType<Italic>();
			((Italic)values[1]).Inlines.Should().HaveCount(1);
			((Italic)values[1]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Italic)values[1]).Inlines.ElementAt(0)).Text.Should().Be("o");
			values[2].Should().BeOfType<Run>();
			((Run)values[2]).Text.Should().Be("o");
		}

		[Test]
		public void TestNormalBold()
		{
			var parser = new MarkdownParser();
			var values = parser.Parse("hello, **world!**");
			values.Should().HaveCount(2);
			values[0].Should().BeOfType<Run>();
			((Run)values[0]).Text.Should().Be("hello, ");
			values[1].Should().BeOfType<Bold>();
			((Bold)values[1]).Inlines.Should().HaveCount(1);
			((Bold)values[1]).Inlines.ElementAt(0).Should().BeOfType<Run>();
			((Run)((Bold)values[1]).Inlines.ElementAt(0)).Text.Should().Be("world!");
		}
	}
}