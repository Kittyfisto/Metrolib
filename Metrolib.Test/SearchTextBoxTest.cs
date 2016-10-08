using System;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class SearchTextBoxTest
	{
		[Test]
		[STAThread]
		public void TestChangeText1()
		{
			var box = new SearchTextBox {RequiresExplicitSearchStart = false};
			box.IsPerformingSearch.Should().BeFalse("because we haven't entered any text");
			box.Text = "Foobar";
			box.IsPerformingSearch.Should()
			   .BeTrue("because searches should start automatically when 'RequiresExplicitSearchStart' is set to false");

			box.Text = null;
			box.IsPerformingSearch.Should().BeFalse("because we've cleared the text again");
		}
	}
}