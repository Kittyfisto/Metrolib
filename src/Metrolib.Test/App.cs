using System.Windows;

namespace Metrolib.Test
{
	internal sealed class App
		: Application
	{
		public static readonly App Instance;

		static App()
		{
			Instance = new App();
		}
	}
}
