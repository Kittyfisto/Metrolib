using System.Xml.Linq;

namespace DocumentationCreator
{
	public sealed class TypeDocumentation
		: MemberDocumentation
	{
		public readonly string FullTypeName;

		public TypeDocumentation(XElement member) : base(member)
		{
			FullTypeName = member.Attribute("name").Value.Substring(2);
		}
	}
}