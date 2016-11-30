using System;
using System.Collections.Generic;

namespace Metrolib.Controls.Charts.Network.Layout
{
	internal interface INodeLayoutAlgorithm
		: IDisposable
	{
		void Update(TimeSpan dt, List<Node> nodes);

		void AddNode(object node);
		void RemoveNode(object node);
		void ClearNodes();

		void AddEdges(IEnumerable<IEdge> edges);
		void RemoveEdges(IEnumerable<IEdge> edges);
		void ClearEdges();
	}
}