using System;
using System.IO;
using System.Reflection;

namespace Metrolib
{
	/// <summary>
	///     Base class for any bootstrapper to get the application running.
	/// </summary>
	/// <remarks>
	///     Responsible for:
	///     - Loading assemblies from embedded resources
	///     - (Live) Auto updating (TODO)
	/// </remarks>
	public class AbstractBootstrapper
	{
		private static string _containingAssembly;
		private static string _subFolder;

		/// <summary>
		/// Allows 3rd party assemblies to be resolved from an embedded resources in the given assembly under
		/// %Assembly%\%subfolder%\
		/// </summary>
		/// <param name="containingAssembly"></param>
		/// <param name="subFolder"></param>
		protected static void EnableEmbeddedDependencyLoading(string containingAssembly, string subFolder)
		{
			_containingAssembly = containingAssembly;
			_subFolder = subFolder;

			AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
		}

		private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
		{
			string name = args.Name;

			var assemblyName = new AssemblyName(name);
			string fileName = assemblyName.Name;

			string resource = string.Format("{0}.{1}.{2}.dll", _containingAssembly, _subFolder, fileName);
			Assembly curAsm = Assembly.GetExecutingAssembly();
			using (Stream stream = curAsm.GetManifestResourceStream(resource))
			{
				if (stream == null)
					return null;

				byte[] data = ReadFully(stream);
				return Assembly.Load(data);
			}
		}

		private static byte[] ReadFully(Stream input)
		{
			var buffer = new byte[16*1024];
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