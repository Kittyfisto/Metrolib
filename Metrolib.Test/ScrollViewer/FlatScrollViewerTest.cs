using System;
using System.Windows.Controls;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.ScrollViewer
{
	[LocalTest]
	[TestFixture]
	public sealed class FlatScrollViewerTest
	{
		private FlatScrollViewer _scrollViewer;

		[SetUp]
		[STAThread]
		public void SetUp()
		{
			_scrollViewer = new FlatScrollViewer();
		}

		[Test]
		[STAThread]
		public void TestCtor()
		{
			_scrollViewer.MousePanningMode.Should().Be(PanningMode.None);
		}
	}
}