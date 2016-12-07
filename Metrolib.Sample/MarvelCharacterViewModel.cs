using System;

namespace Metrolib.Sample
{
	public sealed class MarvelCharacterViewModel
		: INode
	{
		public string Name { get; set; }
		public Uri Portrait { get; set; }

		public override string ToString()
		{
			return Name;
		}
	}
}