using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Netstat.BusinessLogic
{
	public sealed class Engine
		: INetstatListener
		  , IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private readonly HashSet<TcpConnectionDescriptor> _currentConnections;
		private readonly object _syncRoot;
		private readonly Task _task;

		private bool _isDisposed;

		public Engine()
		{
			_syncRoot = new object();
			_currentConnections = new HashSet<TcpConnectionDescriptor>(new TcpConnectionEqualityComparer());

			// TODO: Replace with periodic task scheduler
			_task = new Task(Run);
			_task.Start();
		}

		public IEnumerable<TcpConnectionDescriptor> AllConnections
		{
			get
			{
				lock (_syncRoot)
				{
					return _currentConnections.ToList();
				}
			}
		}

		public void Dispose()
		{
			_isDisposed = true;
		}

		public void OnChanged(TcpConnectionDescriptor descriptor)
		{
			lock (_syncRoot)
			{
				_currentConnections.Add(descriptor);
			}
		}

		private void Run()
		{
			while (!_isDisposed)
			{
				try
				{
					RunOnce();
					Thread.Sleep(TimeSpan.FromSeconds(1));
				}
				catch (Exception e)
				{
					Log.ErrorFormat("Caught unexpected exception: {0}", e);
				}
			}
		}

		private void RunOnce()
		{
			using (var netstat = new Netstat())
			{
				netstat.AddListener(this);
				netstat.Run();
			}
		}
	}
}