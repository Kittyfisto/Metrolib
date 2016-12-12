using System;
using System.IO;
using System.Reflection;
using System.Windows;
using Metrolib;
using Netstat.BusinessLogic;
using log4net;

namespace Netstat
{
	public sealed class App
		: Application
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private static readonly string ApplicationSettingsFolder;
		private static readonly string ApplicationSettingsFileName;

		static App()
		{
			string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			ApplicationSettingsFolder = Path.Combine(appData, "Netstat");
			ApplicationSettingsFileName = Path.Combine(ApplicationSettingsFolder, "Settings.xml");
		}

		[STAThread]
		public static int Main(string[] args)
		{
			var app = new App();
			using (var engine = new Engine())
			{
				var viewModel = new MainWindowViewModel(engine);
				var window = new MainWindow(viewModel);

				var settings = new ApplicationSettings();
				try
				{
					settings.RestoreFrom(ApplicationSettingsFileName);
					settings.MainWindowSettings.RestoreTo(window);
				}
				catch (Exception e)
				{
					Log.ErrorFormat("Caught unexpected exception: {0}", e);
				}

				window.Show();
				int ret = app.Run();

				settings.MainWindowSettings.UpdateFrom(window);
				SaveSettings(settings);

				return ret;
			}
		}

		private static void SaveSettings(ApplicationSettings settings)
		{
			try
			{
				settings.Save(ApplicationSettingsFileName);
			}
			catch (Exception e)
			{
				Log.ErrorFormat("Caught unexpected exception: {0}", e);
			}
		}
	}
}