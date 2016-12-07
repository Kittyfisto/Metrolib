using System;
using System.Windows;
using System.Windows.Controls;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test.ScrollViewer
{
	[TestFixture]
	[LocalTest]
	public sealed class FlatScrollViewerStyleTest
	{
		[SetUp]
		[STAThread]
		public void SetUp()
		{
			_scrollViewer = new FlatScrollViewer {Style = StyleHelper.Load<FlatScrollViewer>()};
		}

		private FlatScrollViewer _scrollViewer;

		[Test]
		[STAThread]
		public void TestCtor()
		{
			_scrollViewer.MousePanningMode.Should().Be(PanningMode.None);
		}

		[Test]
		[STAThread]
		public void TestScroll1()
		{
			_scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			_scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

			var child = new FrameworkElement {MinHeight = 500, MinWidth = 500};
			_scrollViewer.Content = child;

			var availableSize = new System.Windows.Size(100, 100);
			_scrollViewer.Measure(availableSize);
			_scrollViewer.Arrange(new Rect(availableSize));

			_scrollViewer.ComputedHorizontalScrollBarVisibility.Should().Be(Visibility.Visible);
			_scrollViewer.ComputedVerticalScrollBarVisibility.Should().Be(Visibility.Visible);

			_scrollViewer.HorizontalOffset.Should().Be(0);
			_scrollViewer.VerticalOffset.Should().Be(0);
		}
	}
}