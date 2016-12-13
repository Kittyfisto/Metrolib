using System;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Pie
{
	[TestFixture]
	public sealed class PieChartValueItemTest
	{
		[Test]
		[STAThread]
		public void TestCtor()
		{
			var item = new PieChartValueItem();
			item.Content.Should().BeNull();
		}
	}
}