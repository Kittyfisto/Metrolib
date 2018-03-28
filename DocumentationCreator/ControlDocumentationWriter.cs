using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DocumentationCreator
{
	/// <summary>
	///     Responsible for writing the documentation for a single control.
	///     Extracts information from the XML documentation which accompanies a .NET assembly,
	///     but can be enhanced by examples, images, etc...
	/// </summary>
	internal sealed class ControlDocumentationWriter<T>
		: IWriter
		where T : FrameworkElement
	{
		private readonly AssemblyDocumentationReader _assemblyDocumentationReader;
		private readonly TypeDocumentation _typeDocumentation;
		private readonly List<IWriter> _subWriters;

		public ControlDocumentationWriter(AssemblyDocumentationReader assemblyDocumentationReader)
		{
			if (assemblyDocumentationReader == null)
				throw new ArgumentNullException(nameof(assemblyDocumentationReader));

			_assemblyDocumentationReader = assemblyDocumentationReader;
			_typeDocumentation = assemblyDocumentationReader.GetDocumentationOf<T>();
			_subWriters = new List<IWriter>();
		}

		public ExampleWriter AddExample(string name)
		{
			var writer = new ExampleWriter(name);
			_subWriters.Add(writer);
			return writer;
		}

		public void WriteTo(TextWriter textWriter)
		{
			textWriter.WriteHeader(typeof(T).Name);
			textWriter.WriteSummary(_typeDocumentation);
			foreach (var subWriter in _subWriters)
			{
				subWriter.WriteTo(textWriter);
			}
		}
	}
}