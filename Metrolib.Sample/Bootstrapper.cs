using System;
using System.IO;
using System.Windows;

namespace Metrolib.Sample
{
	public class Bootstrapper
		: AbstractBootstrapper
	{
		private static readonly string ApplicationSettingsFolder;
		private static readonly string ApplicationSettingsFileName;

		static Bootstrapper()
		{
			var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			ApplicationSettingsFolder = Path.Combine(appData, "Metrolib Sample");
			ApplicationSettingsFileName = Path.Combine(ApplicationSettingsFolder, "Settings.xml");
		}

		[STAThread]
		public static int Main(string[] args)
		{
			var app = new Application();
			var window = new MainWindow();

			var settings = new ApplicationSettings();
			try
			{
				settings.RestoreFrom(ApplicationSettingsFileName);
				settings.MainWindowSettings.RestoreTo(window);
			}
			catch (Exception)
			{}

			window.Show();
			int ret = app.Run();

			settings.MainWindowSettings.UpdateFrom(window);
			SaveSettings(settings);

			return ret;
		}

		private static void SaveSettings(ApplicationSettings settings)
		{
			try
			{
				settings.Save(ApplicationSettingsFileName);
			}
			catch (Exception e)
			{
				
			}
		}
	}
}