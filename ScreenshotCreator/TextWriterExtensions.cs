using System.IO;

namespace ScreenshotCreator
{
	public static class TextWriterExtensions
	{
		public static void WriteHeader(this TextWriter writer, string name)
		{
			writer.WriteLine("# {0}", name);
			writer.WriteLine();
		}

		public static void WriteSummary(this TextWriter writer, MemberDocumentation documentation)
		{
			if (documentation != null)
			{
				var summary = documentation.Summary ?? string.Empty;
				writer.Write(summary);
				writer.WriteLine();
				if (!summary.EndsWith("\n"))
					writer.WriteLine();
			}
		}

		public static void WriteImage(this TextWriter writer, string relativeImagePath, string description)
		{
			
		}
	}
}