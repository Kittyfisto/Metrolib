using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using log4net;

namespace Metrolib.Settings
{
	/// <summary>
	///     Represents all settings an application wants to store.
	///     Can be subclassed to add application specific properties.
	/// </summary>
	public class ApplicationSettings
		: ICloneable
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		/// <summary>
		///     The settings of the main window of the application (position, dimensions, etc...)
		/// </summary>
		public WindowSettings MainWindowSettings;

		/// <summary>
		///     Default ctor.
		/// </summary>
		public ApplicationSettings()
		{
			MainWindowSettings = new WindowSettings();
		}

		/// <summary>
		///     Ctor intended to be used by subclasses in order to create a clone.
		/// </summary>
		/// <param name="applicationSettings"></param>
		protected ApplicationSettings(ApplicationSettings applicationSettings)
		{
			WindowSettings mainWindowSettings = applicationSettings.MainWindowSettings;
			MainWindowSettings = mainWindowSettings != null ? mainWindowSettings.Clone() : null;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}

		/// <summary>
		///     Restores this settings from the given file.
		/// </summary>
		/// <param name="fileName"></param>
		public void RestoreFrom(string fileName)
		{
			using (FileStream stream = File.OpenRead(fileName))
			using (XmlReader reader = XmlReader.Create(stream))
			{
				while (reader.Read())
				{
					Read(reader);
				}
			}
		}

		/// <summary>
		///     Is called repeatedly from <see cref="RestoreFrom" /> for every top level
		///     node written by <see cref="Write" />.
		/// </summary>
		/// <param name="reader"></param>
		protected virtual void Read(XmlReader reader)
		{
			switch (reader.Name)
			{
				case "mainwindow":
					MainWindowSettings.Restore(reader);
					break;
			}
		}

		/// <summary>
		///     Saves this object to disk.
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns>A task that completes when the settings have been saved.</returns>
		public Task SaveAsync(string fileName)
		{
			if (fileName == null)
				throw new ArgumentNullException("fileName");

			// We want to create a clone because we do not want any side effects
			// from callers modifying this object after having called this method.
			ApplicationSettings clone = Clone();
			return Task.Factory.StartNew(() => clone.Save(fileName));
		}

		/// <summary>
		///     Saves this object to disk.
		/// </summary>
		/// <param name="fileName"></param>
		public void Save(string fileName)
		{
			using (var stream = new MemoryStream())
			{
				var xmlSettings = new XmlWriterSettings
					{
						Indent = true,
						IndentChars = "  ",
						NewLineChars = "\r\n",
						NewLineHandling = NewLineHandling.Replace
					};
				using (XmlWriter writer = XmlWriter.Create(stream, xmlSettings))
				{
					writer.WriteStartElement("xml");

					Write(writer);

					writer.WriteEndElement();
				}

				string directory = Path.GetDirectoryName(fileName);

				Log.DebugFormat("Saving to directory '{0}'", directory);

				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);

				using (var file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
				{
					var length = (int) stream.Position;
					file.Write(stream.GetBuffer(), 0, length);
					file.SetLength(length);
				}
			}
		}

		/// <summary>
		///     Is eventually invoked when <see cref="SaveAsync" /> has been called.
		/// </summary>
		/// <param name="writer"></param>
		protected virtual void Write(XmlWriter writer)
		{
			writer.WriteStartElement("mainwindow");
			MainWindowSettings.Save(writer);
			writer.WriteEndElement();
		}

		/// <summary>
		///     Creates a clone.
		/// </summary>
		/// <returns></returns>
		public virtual ApplicationSettings Clone()
		{
			return new ApplicationSettings(this);
		}
	}
}