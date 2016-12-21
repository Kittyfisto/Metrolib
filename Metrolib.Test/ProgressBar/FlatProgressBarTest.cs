using NUnit.Framework;

namespace Metrolib.Test.ProgressBar
{
	[TestFixture]
	public sealed class FlatProgressBarTest
		: AbstractProgressBarTest
	{
		protected override AbstractProgressBar Create()
		{
			return new FlatProgressBar();
		}
	}
}