using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using log4net;

namespace DocumentationCreator
{
	/// <summary>
	///     Responsible for reading the documentation of a .NET assembly (which resides in a similarly named xml file,
	///     usually).
	/// </summary>
	public sealed class AssemblyDocumentationReader
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly Assembly _assembly;
		private readonly string _assemblyDocumentationFilePath;
		private readonly string _assemblyFilePath;
		private readonly XDocument _document;
		private readonly Dictionary<Type, TypeDocumentation> _typeDocumenstationsByType;
		private readonly Dictionary<string, TypeDocumentation> _typeDocumenstationsByName;

		public AssemblyDocumentationReader(Assembly assembly)
		{
			_assembly = assembly;

			var codeBase = assembly.CodeBase;
			var uri = new UriBuilder(codeBase);
			_assemblyFilePath = Uri.UnescapeDataString(uri.Path);
			var assemblyFileName = Path.GetFileNameWithoutExtension(_assemblyFilePath);
			_assemblyDocumentationFilePath =
				Path.Combine(Path.GetDirectoryName(_assemblyFilePath), string.Format("{0}.xml", assemblyFileName));
			_document = XDocument.Load(_assemblyDocumentationFilePath);

			_typeDocumenstationsByType = new Dictionary<Type, TypeDocumentation>();
			_typeDocumenstationsByName = new Dictionary<string, TypeDocumentation>();

			foreach (var member in _document.Descendants("member"))
			{
				var name = member.Attribute("name")?.Value ?? string.Empty;
				if (name.StartsWith("T:"))
				{
					var typeDocumentation = TypeDocumentation.CreateFrom(member, assembly);

					_typeDocumenstationsByName.Add(typeDocumentation.FullTypeName, typeDocumentation);
					var type = assembly.GetType(typeDocumentation.FullTypeName);
					if (type != null)
					{
						_typeDocumenstationsByType.Add(type, typeDocumentation);
					}
					else
					{
						Log.WarnFormat("Unable to find type '{0}' in assembly '{1}'", typeDocumentation.FullTypeName, assemblyFileName);
					}
				}

				if (name.StartsWith("P:"))
				{
					var propertyDocumentation = PropertyDocumentation.CreateFrom(member, assembly);
					if (_typeDocumenstationsByName.TryGetValue(propertyDocumentation.FullTypeName, out var typeDocumentation))
					{
						typeDocumentation.Add(propertyDocumentation);
					}
					else
					{
						Log.WarnFormat("Unable to find type '{0}' in documentation", propertyDocumentation.FullTypeName);
					}
				}
			}
		}

		public TypeDocumentation GetDocumentationOf(Type type)
		{
			_typeDocumenstationsByType.TryGetValue(type, out var documentation);
			return documentation;
		}

		public TypeDocumentation GetDocumentationOf<T>()
		{
			return GetDocumentationOf(typeof(T));
		}
	}
}