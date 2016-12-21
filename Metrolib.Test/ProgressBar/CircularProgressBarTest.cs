using NUnit.Framework;

namespace Metrolib.Test.ProgressBar
{
	[TestFixture]
	public sealed class CircularProgressBarTest
		: AbstractProgressBarTest
	{
		protected override AbstractProgressBar Create()
		{
			return new CircularProgressBar();
		}
	}
}