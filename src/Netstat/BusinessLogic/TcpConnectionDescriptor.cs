using System;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;

namespace Netstat.BusinessLogic
{
	public sealed class TcpConnectionDescriptor
	{
		public readonly EndPoint ForeignEndPoint;
		public readonly EndPoint LocalEndPoint;
		public readonly TcpState State;

		public TcpConnectionDescriptor(EndPoint localEndPoint, EndPoint foreignEndPoint, TcpState state)
		{
			LocalEndPoint = localEndPoint;
			ForeignEndPoint = foreignEndPoint;
			State = state;
		}

		public override string ToString()
		{
			return string.Format("{0} {1} {2}", LocalEndPoint, ForeignEndPoint, State);
		}

		public static bool TryParse(string value, out TcpConnectionDescriptor descriptor)
		{
			if (value != null)
			{
				string[] values = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				EndPoint localAddress, foreignAddress;
				TcpState state;
				if (values.Length == 4 &&
					values[0] == "TCP" &&
					TryParse(values[1], out localAddress) &&
					TryParse(values[2], out foreignAddress) &&
					TryParse(values[3], out state))
				{
					descriptor = new TcpConnectionDescriptor(localAddress, foreignAddress, state);
					return true;
				}
			}

			descriptor = null;
			return false;
		}

		private static bool TryParse(string value, out TcpState state)
		{
			switch (value)
			{
				case "LISTENING":
					state = TcpState.Listen;
					return true;

				case "ESTABLISHED":
					state = TcpState.Established;
					return true;

				case "TIME_WAIT":
					state = TcpState.TimeWait;
					return true;

				default:
					state = TcpState.Unknown;
					return false;
			}
		}

		private static bool TryParse(string value, out EndPoint endPoint)
		{
			string[] values = value.Split(new[] {':'});
			ushort port;

			if (values.Length == 2 &&
				ushort.TryParse(values[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out port))
			{
				IPAddress address;
				if (IPAddress.TryParse(values[0], out address))
				{
					endPoint = new IPEndPoint(address, port);
				}
				else
				{
					endPoint = new DnsEndPoint(values[0], port);
				}

				return true;
			}

			endPoint = null;
			return false;
		}
	}
}