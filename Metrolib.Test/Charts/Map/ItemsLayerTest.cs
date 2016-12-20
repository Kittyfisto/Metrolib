using System;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Map
{
	[TestFixture]
	public sealed class ItemsLayerTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var layer = new ItemsLayer();
			layer.ItemTemplate.Should().BeNull();
			layer.ItemTemplateSelector.Should().BeNull();
			layer.ItemsSource.Should().BeNull();
		}
	}
}