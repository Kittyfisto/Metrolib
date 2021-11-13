using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class NetworkViewNodeItemTest
	{
		private NetworkViewNodeItem _item;

		[SetUp]
		public void SetUp()
		{
			_item = new NetworkViewNodeItem();
		}

		[Test]
		public void TestCtor()
		{
			_item.Content.Should().BeNull();
			_item.IsSelected.Should().BeFalse();
		}
	}
}