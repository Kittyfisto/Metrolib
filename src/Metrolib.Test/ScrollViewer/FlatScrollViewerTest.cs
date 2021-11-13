using System.Threading;
using System.Windows.Controls;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.ScrollViewer
{
	[LocalTest]
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class FlatScrollViewerTest
	{
		private FlatScrollViewer _scrollViewer;

		[SetUp]
		public void SetUp()
		{
			_scrollViewer = new FlatScrollViewer();
		}

		[Test]
		public void TestCtor()
		{
			_scrollViewer.MousePanningMode.Should().Be(PanningMode.None);
		}
	}
}