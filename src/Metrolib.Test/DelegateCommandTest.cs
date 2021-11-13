using FluentAssertions;
using NUnit.Framework;

namespace Metrolib.Test
{
	[TestFixture]
	public sealed class DelegateCommandTest
	{
		[Test]
		public void TestCanExecute1()
		{
			var command = new DelegateCommand<string>(value => { }, value =>
				{
					value.Should().BeNull();
					return true;
				});

			command.CanExecute(42).Should().BeTrue("Because the delegate shall be invoked even when the types don't match");
		}
	}
}