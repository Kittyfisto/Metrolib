using System.IO;

namespace DocumentationCreator
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
				writer.WriteLine();
			}
		}

		public static void WriteRemarks(this TextWriter writer, MemberDocumentation documentation)
		{
			if (documentation != null)
			{
				foreach (var remark in documentation.Remarks)
				{
					writer.Write(remark);
					writer.WriteLine();
					writer.WriteLine();
				}
			}
		}
	}
}