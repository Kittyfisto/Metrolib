using System;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.Tree
{
	[TestFixture]
	public sealed class FlatTreeViewItemTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var item = new FlatTreeViewItem();
			item.IsExpandable.Should().BeTrue();
		}
	}
}