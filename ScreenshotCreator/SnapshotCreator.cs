using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ScreenshotCreator
{
	public sealed class SnapshotCreator<T>
		: ISnapshotCreator
		where T : FrameworkElement, new()
	{
		private readonly Dispatcher _dispatcher;
		private readonly ResourceDictionary _resourceDictionary;
		private readonly Dictionary<string, BitmapSource> _snapshots;

		public SnapshotCreator(Dispatcher dispatcher, ResourceDictionary resourceDictionary)
		{
			_dispatcher = dispatcher;
			_resourceDictionary = resourceDictionary;
			_snapshots = new Dictionary<string, BitmapSource>();
		}

		[Pure]
		public PoseSnapshot<T> AddPose(string pose)
		{
			if (pose == null)
				throw new ArgumentNullException(nameof(pose));

			return new PoseSnapshot<T>(this, _dispatcher, _resourceDictionary, pose);
		}

		internal void Add(BitmapSource screenshot, string pose)
		{
			_snapshots.Add(pose, screenshot);
		}

		public void SaveAllSnapshots(string basePath)
		{
			var elementName = typeof(T).Name;

			foreach (var pair in _snapshots)
			{
				var pose = pair.Key;
				var bitmap = pair.Value;

				var destination = Path.Combine(basePath, elementName, string.Format("{0}.png", pose));
				SaveSnapshot(bitmap, destination);
			}
		}

		private static void SaveSnapshot(BitmapSource screenshot, string destination)
		{
			var encoder = new PngBitmapEncoder();
			using (var stream = new MemoryStream())
			{
				var frame = BitmapFrame.Create(screenshot);
				encoder.Frames.Add(frame);
				encoder.Save(stream);

				stream.Position = 0;

				var destinationDirectory = Path.GetDirectoryName(destination);
				Directory.CreateDirectory(destinationDirectory);
				using (var fileStream = File.OpenWrite(destination))
				{
					stream.CopyTo(fileStream);
				}
			}
		}
	}
}