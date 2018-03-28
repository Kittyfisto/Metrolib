using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace DocumentationCreator
{
	public sealed class TypeDocumentation
		: MemberDocumentation
	{
		public readonly Type Type;
		public readonly string FullTypeName;
		public readonly IReadOnlyList<PropertyDocumentation> Properties;
		private readonly List<PropertyDocumentation> _properties;

		public static TypeDocumentation CreateFrom(XElement member, Assembly assembly)
		{
			var fullTypeName = member.Attribute("name").Value.Substring(2);
			var type = assembly.GetType(fullTypeName);
			return new TypeDocumentation(type, fullTypeName, GetSummary(member), GetRemarks(member));
		}

		public TypeDocumentation(Type type, string fullTypeName, string summary, IReadOnlyList<string> remarks) : base(summary, remarks)
		{
			Type = type;
			FullTypeName = fullTypeName;
			Properties = _properties = new List<PropertyDocumentation>();
		}

		public void Add(PropertyDocumentation property)
		{
			_properties.Add(property);
		}
	}
}