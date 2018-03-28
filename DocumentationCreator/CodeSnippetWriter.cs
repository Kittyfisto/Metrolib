using System.IO;

namespace DocumentationCreator
{
	internal sealed class CodeSnippetWriter
		: StringWriter
		, IWriter
	{
		private readonly string _language;

		public CodeSnippetWriter(string language)
		{
			_language = language;
		}

		public void WriteTo(TextWriter textWriter)
		{
			textWriter.WriteLine("```{0}", _language);
			textWriter.Write(ToString());
			textWriter.WriteLine("```");
		}
	}
}