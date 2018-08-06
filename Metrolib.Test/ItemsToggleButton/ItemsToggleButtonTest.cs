using System.Threading;
using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test.ItemsToggleButton
{
	[TestFixture]
	[RequiresThread(ApartmentState.STA)]
	public sealed class ItemsToggleButtonTest
	{
		[Test]
		public void SetNullItemsSource()
		{
			var control = new Controls.ItemsToggleButton();
			control.ItemsSource = new object[] {42};
			control.ItemsSource.Should().Equal(42);

			control.ItemsSource = null;
			control.ItemsSource.Should().BeNull();
		}
	}
}
