using System.IO;

namespace ScreenshotCreator
{
	internal interface IWriter
	{
		void WriteTo(TextWriter textWriter);
	}
}