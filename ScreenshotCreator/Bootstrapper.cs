using System;
using System.Reflection;
using log4net;

namespace ScreenshotCreator
{
	static class Bootstrapper
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		[STAThread]
		public static int Main(string[] arguments)
		{
			try
			{
				return Application2.Run();
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Caught unexpected exception: {0}", e);
				return -1;
			}
		}
	}
}
