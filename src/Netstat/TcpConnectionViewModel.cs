using System.Net.NetworkInformation;
using Metrolib;

namespace Netstat
{
	public sealed class TcpConnectionViewModel
		: IEdge
	{
		private readonly EndPointViewModel _localEndPoint;
		private readonly EndPointViewModel _foreignEndPoint;
		private readonly TcpState _state;

		public TcpConnectionViewModel(EndPointViewModel localEndPoint, EndPointViewModel foreign, TcpState state)
		{
			_localEndPoint = localEndPoint;
			_foreignEndPoint = foreign;
			_state = state;
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", _localEndPoint, _foreignEndPoint, _state);
		}

		public EdgeDirection Direction
		{
			get { return EdgeDirection.Node1ToNode2; }
		}

		public INode Node1
		{
			get { return _localEndPoint; }
		}

		public INode Node2
		{
			get { return _foreignEndPoint; }
		}
	}
}