using System.IO;

namespace DocumentationCreator
{
	internal sealed class ImageWriter
		: IWriter
	{
		private readonly string _description;
		private readonly string _relativeImagePath;

		public ImageWriter(string description, string relativeImagePath)
		{
			_description = description;
			_relativeImagePath = relativeImagePath;
		}

		public void WriteTo(TextWriter textWriter)
		{
			textWriter.WriteLine("![{0}]({1})", _description, _relativeImagePath);
		}
	}
}