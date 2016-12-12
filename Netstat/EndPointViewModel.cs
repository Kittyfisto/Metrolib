using System;
using System.Net;
using Metrolib;

namespace Netstat
{
	public sealed class EndPointViewModel
		: INode
	{
		private readonly EndPoint _endPoint;

		public EndPointViewModel(EndPoint endPoint)
		{
			if (endPoint == null)
				throw new ArgumentNullException("endPoint");

			_endPoint = endPoint;
		}

		public override string ToString()
		{
			return _endPoint.ToString();
		}

		public string Name
		{
			get { return ToString(); }
		}
	}
}