using System;
using System.Collections;
using System.Collections.Generic;

namespace Metrolib.Controls.Charts.Network.Layout
{
	internal interface INodeLayoutAlgorithm
		: IDisposable
	{
		void Update(TimeSpan dt, List<Node> nodes);

		void AddNodes(IEnumerable nodes);
		void RemoveNodes(IEnumerable nodes);
		void ClearNodes();

		void AddEdges(IEnumerable<IEdge> edges);
		void RemoveEdges(IEnumerable<IEdge> edges);
		void ClearEdges();
	}
}