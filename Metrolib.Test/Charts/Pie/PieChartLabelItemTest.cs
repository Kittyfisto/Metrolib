using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.Charts.Pie
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class PieChartLabelItemTest
	{
		[Test]
		public void TestCtor()
		{
			var item = new PieChartLabelItem();
			item.Content.Should().BeNull();
		}
	}
}