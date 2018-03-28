using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DocumentationCreator
{
	public class MemberDocumentation
	{
		public readonly string Summary;
		public readonly IReadOnlyList<string> Remarks;

		public MemberDocumentation(XElement member)
		{
			var summary = member.Descendants("summary").FirstOrDefault();
			Summary = summary?.Value.Trim();

			var remarks = member.Descendants("remarks");
			var allRemarks = new List<string>();
			foreach (var remark in remarks)
			{
				allRemarks.Add(remark.Value.Trim());
			}

			Remarks = allRemarks;
		}
	}
}