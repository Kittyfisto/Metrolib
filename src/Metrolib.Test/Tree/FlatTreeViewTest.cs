using System.Threading;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.Tree
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class FlatTreeViewTest
	{
		[Test]
		public void TestCtor()
		{
			var tree = new FlatTreeView();
			tree.IsExpandable.Should().BeTrue();
		}
	}
}