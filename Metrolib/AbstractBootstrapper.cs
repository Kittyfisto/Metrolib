using System;
using System.IO;
using System.Reflection;

namespace Metrolib
{
	/// <summary>
	/// Base class for any bootstrapper to get the application running.
	/// </summary>
	/// <remarks>
	/// Responsible for:
	/// - Loading assemblies from embedded resources
	/// - (Live) Auto updating (TODO)
	/// </remarks>
	public class AbstractBootstrapper
	{
		protected static void SetupDependencies()
		{
			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
		}

		protected static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
		{
			var name = args.Name;

			var assemblyName = new AssemblyName(name);
			string fileName = assemblyName.Name;

			string resource = string.Format("Tailviewer.ThirdParty.{0}.dll", fileName);
			Assembly curAsm = Assembly.GetExecutingAssembly();
			using (Stream stream = curAsm.GetManifestResourceStream(resource))
			{
				if (stream == null)
					return null;

				var data = ReadFully(stream);
				return Assembly.Load(data);
			}
		}

		private static byte[] ReadFully(Stream input)
		{
			var buffer = new byte[16 * 1024];
			using (var ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}
	}
}