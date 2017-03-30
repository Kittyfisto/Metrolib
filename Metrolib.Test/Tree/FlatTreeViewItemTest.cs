using System.Threading;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.Tree
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class FlatTreeViewItemTest
	{
		[Test]
		public void TestCtor()
		{
			var item = new FlatTreeViewItem();
			item.IsExpandable.Should().BeTrue();
		}
	}
}