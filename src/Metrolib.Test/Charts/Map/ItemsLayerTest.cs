using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Map
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class ItemsLayerTest
	{
		[Test]
		public void TestCtor()
		{
			var layer = new ItemsLayer();
			layer.ItemTemplate.Should().BeNull();
			layer.ItemTemplateSelector.Should().BeNull();
			layer.ItemsSource.Should().BeNull();
		}
	}
}