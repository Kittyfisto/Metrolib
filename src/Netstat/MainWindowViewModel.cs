using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using Metrolib;
using Netstat.BusinessLogic;

namespace Netstat
{
	public sealed class MainWindowViewModel
		: INotifyPropertyChanged
	{
		private readonly Engine _engine;

		#region EndPoints

		private readonly ObservableCollection<INode> _nodes;
		private readonly Dictionary<EndPoint, EndPointViewModel> _endPointsToViewModel;

		#endregion

		#region Connections

		private readonly ObservableCollection<IEdge> _edges;
		private readonly Dictionary<TcpConnectionDescriptor, TcpConnectionViewModel> _connectionsToViewModel;
		private readonly INode _localComputer;

		#endregion

		public MainWindowViewModel(Engine engine)
		{
			_engine = engine;

			_endPointsToViewModel = new Dictionary<EndPoint, EndPointViewModel>();
			_localComputer = new LocalComputerViewModel();
			_nodes = new ObservableCollection<INode>
				{
					_localComputer
				};

			_connectionsToViewModel =
				new Dictionary<TcpConnectionDescriptor, TcpConnectionViewModel>(new TcpConnectionEqualityComparer());
			_edges = new ObservableCollection<IEdge>();
		}

		public IEnumerable<IEdge> Edges
		{
			get { return _edges; }
		}

		public IEnumerable<INode> Nodes
		{
			get { return _nodes; }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void EmitPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Update()
		{
			foreach (TcpConnectionDescriptor connection in _engine.AllConnections)
			{
				bool added;
				EndPointViewModel local = AddOrUpdate(connection.LocalEndPoint, out added);
				if (added)
				{
					_edges.Add(new Edge {Node1 = _localComputer, Node2 = local});
				}

				bool unused;
				EndPointViewModel foreign = AddOrUpdate(connection.ForeignEndPoint, out unused);

				AddOrUpdate(connection, local, foreign);
			}
		}

		private EndPointViewModel AddOrUpdate(EndPoint endPoint, out bool added)
		{
			EndPointViewModel viewModel;
			if (!_endPointsToViewModel.TryGetValue(endPoint, out viewModel))
			{
				viewModel = new EndPointViewModel(endPoint);
				_endPointsToViewModel.Add(endPoint, viewModel);
				_nodes.Add(viewModel);
				added = true;
			}
			else
			{
				added = false;
			}

			return viewModel;
		}

		private void AddOrUpdate(TcpConnectionDescriptor connection, EndPointViewModel local, EndPointViewModel foreign)
		{
			TcpConnectionViewModel viewModel;
			if (!_connectionsToViewModel.TryGetValue(connection, out viewModel))
			{
				viewModel = new TcpConnectionViewModel(local, foreign, connection.State);
				_connectionsToViewModel.Add(connection, viewModel);
				_edges.Add(viewModel);
			}
		}
	}
}