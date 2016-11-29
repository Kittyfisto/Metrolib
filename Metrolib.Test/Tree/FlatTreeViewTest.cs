using System;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.Tree
{
	[TestFixture]
	public sealed class FlatTreeViewTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var tree = new FlatTreeView();
			tree.IsExpandable.Should().BeTrue();
		}
	}
}