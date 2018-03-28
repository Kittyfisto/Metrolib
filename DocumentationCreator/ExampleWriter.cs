using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DocumentationCreator
{
	internal sealed class ExampleWriter
		: IWriter
	{
		private readonly string _name;
		private readonly List<IWriter> _subWriters;

		public ExampleWriter(string name)
		{
			_name = name;
			_subWriters = new List<IWriter>();
		}

		public CodeSnippetWriter AddCodeSnippet(string language)
		{
			var writer =  new CodeSnippetWriter(language);
			_subWriters.Add(writer);
			return writer;
		}

		public void AddImage(string description, string relativePath)
		{
			_subWriters.Add(new ImageWriter(description, relativePath));
		}

		public void WriteTo(TextWriter textWriter)
		{
			textWriter.WriteLine("### {0}", _name);
			textWriter.WriteLine();

			foreach (var subWriter in _subWriters)
			{
				subWriter.WriteTo(textWriter);
			}

			if (_subWriters.Any())
				textWriter.WriteLine();
		}
	}
}