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
		[Ignore("Hasn't been fixed yet (parser counts wrong)")]
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
	}
}