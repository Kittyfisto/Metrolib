using System.Collections.Generic;
using System.Linq;

namespace Metrolib.Sample
{
	public sealed class EdgeType
	{
		private readonly IEnumerable<IEdge> _edges;
		private readonly string _name;

		public EdgeType(string name, IEnumerable<IEdge> edges)
		{
			_name = name;
			_edges = edges;
		}

		public string Name
		{
			get { return _name; }
		}

		public IEnumerable<IEdge> Edges
		{
			get { return _edges; }
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} edges", _name, _edges.Count());
		}
	}
}