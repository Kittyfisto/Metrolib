using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;

namespace DocumentationCreator
{
	public sealed class PropertyDocumentation
		: MemberDocumentation
	{
		public readonly Type DeclaringType;
		public readonly PropertyInfo Property;
		public Type PropertyType => Property?.PropertyType;
		public readonly string PropertyName;
		public readonly string FullTypeName;

		public PropertyDocumentation(Type declaringType, PropertyInfo property, string propertyName, string fullTypeName, string summary, IReadOnlyList<string> remarks)
		: base(summary, remarks)
		{
			DeclaringType = declaringType;
			Property = property;
			PropertyName = propertyName;
			FullTypeName = fullTypeName;
		}

		public static PropertyDocumentation CreateFrom(XElement member, Assembly assembly)
		{
			var name = member.Attribute("name").Value;
			var index = name.LastIndexOf(".");
			var propertyName = name.Substring(index + 1);
			var fullTypeName = name.Substring(2, index - 2);
			var declaringType = assembly.GetType(fullTypeName);
			var property = declaringType?.GetProperty(propertyName);

			return new PropertyDocumentation(declaringType, property, propertyName, fullTypeName, GetSummary(member), GetRemarks(member));
		}
	}
}