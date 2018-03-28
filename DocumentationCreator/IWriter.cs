using System.IO;

namespace DocumentationCreator
{
	internal interface IWriter
	{
		void WriteTo(TextWriter textWriter);
	}
}