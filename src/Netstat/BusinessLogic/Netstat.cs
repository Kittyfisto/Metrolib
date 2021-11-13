using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;

namespace Netstat.BusinessLogic
{
	public sealed class Netstat
		: IDisposable
	{
		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private static readonly string NetstatPath;
		private readonly List<INetstatListener> _listeners;

		private readonly Process _process;
		private readonly List<TcpConnectionDescriptor> _result;
		private readonly ProcessStartInfo _startInfo;

		static Netstat()
		{
			NetstatPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "netstat.exe");
		}

		public Netstat()
		{
			_listeners = new List<INetstatListener>();
			_result = new List<TcpConnectionDescriptor>();
			_startInfo = new ProcessStartInfo
				{
					FileName = NetstatPath,
					Arguments = "-a",
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardOutput = true,
				};
			_process = new Process
				{
					StartInfo = _startInfo
				};
			_process.OutputDataReceived += ProcessOnOutputDataReceived;
		}

		public void AddListener(INetstatListener listener)
		{
			lock (_listeners)
			{
				_listeners.Add(listener);
			}
		}

		public void Dispose()
		{
			_process.Dispose();
		}

		private void ProcessOnOutputDataReceived(object sender, DataReceivedEventArgs args)
		{
			TcpConnectionDescriptor descriptor;
			if (TcpConnectionDescriptor.TryParse(args.Data, out descriptor))
			{
				_result.Add(descriptor);

				lock (_listeners)
				{
					foreach (var listener in _listeners)
					{
						listener.OnChanged(descriptor);
					}
				}
			}
			else
			{
				Log.DebugFormat("Unable to parse '{0}'", args.Data);
			}
		}

		public List<TcpConnectionDescriptor> Run()
		{
			Log.DebugFormat("Starting netstat: {0}", _startInfo);

			_process.Start();
			_process.BeginOutputReadLine();
			_process.WaitForExit();

			return _result;
		}
	}
}