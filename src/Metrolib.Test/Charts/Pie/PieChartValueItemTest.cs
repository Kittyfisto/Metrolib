using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Pie
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class PieChartValueItemTest
	{
		[Test]
		public void TestCtor()
		{
			var item = new PieChartValueItem();
			item.Content.Should().BeNull();
		}
	}
}