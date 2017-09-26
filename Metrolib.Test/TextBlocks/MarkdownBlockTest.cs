using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.TextBlocks
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class MarkdownBlockTest
	{
		private MarkdownBlock _control;

		[SetUp]
		public void Setup()
		{
			_control = new MarkdownBlock()
			{
				Style = StyleHelper.Load<MarkdownBlock>()
			};
		}

		[Test]
		public void TestApply1()
		{
			_control.Markdown = null;
			_control.ApplyTemplate();
			GetPartTextBlock().Inlines.Should().BeEmpty();
		}

		[Test]
		public void TestApply2()
		{
			_control.Markdown = "Foobar";
			_control.ApplyTemplate();
			GetPartTextBlock().Inlines.Should().NotBeEmpty();
		}

		private TextBlock GetPartTextBlock()
		{
			var type = _control.GetType();
			var textBox = (TextBlock)type.GetField("_textBlock", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_control);
			return textBox;
		}
	}
}