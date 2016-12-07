using System;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Network
{
	[TestFixture]
	public sealed class NetworkViewNodeItemTest
	{
		private NetworkViewNodeItem _item;

		[SetUp]
		[STAThread]
		public void SetUp()
		{
			_item = new NetworkViewNodeItem();
		}

		[Test]
		[STAThread]
		public void TestCtor()
		{
			_item.Content.Should().BeNull();
			_item.IsSelected.Should().BeFalse();
		}
	}
}