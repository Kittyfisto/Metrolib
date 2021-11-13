using System.Threading;
using System.Windows.Controls;
using FluentAssertions;
using Metrolib.Controls;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class DependencyObjectExtensionsTest
	{
		private Grid _grid;
		private Button _button;
		private FlatButton _flatButton;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_grid = new Grid();
			_grid.Children.Add(_button = new Button());
			_grid.Children.Add(_flatButton = new FlatButton());
		}

		[Test]
		[Description("Verifies that FindFirstAncestorOfType also inspects the given element, not just its children")]
		public void TestFindChildrenOfType1()
		{
			_grid.FindChildrenOfType<Grid>().Should().BeEquivalentTo(new object[] {_grid});
		}

		[Test]
		public void TestFindChildrenOfType2()
		{
			_grid.FindChildrenOfType<Button>().Should().BeEquivalentTo(new object[] { _button, _flatButton });
			_grid.FindChildrenOfType<FlatButton>().Should().BeEquivalentTo(new object[] { _flatButton });
			_grid.FindChildrenOfType<Canvas>().Should().BeEmpty();
		}

		[Test]
		public void TestFindFirstAncestorOfType1()
		{
			_grid.FindFirstAncestorOfType<Grid>().Should().Be(_grid);
			_grid.FindFirstAncestorOfType<Button>().Should().Be(null);
		}

		[Test]
		public void TestFindFirstAncestorOfType2()
		{
			_button.FindFirstAncestorOfType<Grid>().Should().Be(_grid);
			_flatButton.FindFirstAncestorOfType<Button>().Should().Be(_flatButton);
		}
	}
}