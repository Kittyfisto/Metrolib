using System.Collections.Generic;

namespace Netstat.BusinessLogic
{
	/// <summary>
	///     Compares two tcp descriptors for equality based on their local / foreign endpoints.
	/// </summary>
	public sealed class TcpConnectionEqualityComparer
		: IEqualityComparer<TcpConnectionDescriptor>
	{
		public bool Equals(TcpConnectionDescriptor x, TcpConnectionDescriptor y)
		{
			return Equals(x.ForeignEndPoint, y.ForeignEndPoint) &&
			       Equals(x.LocalEndPoint, y.LocalEndPoint);
		}

		public int GetHashCode(TcpConnectionDescriptor value)
		{
			unchecked
			{
				int hashCode = (value.ForeignEndPoint != null ? value.ForeignEndPoint.GetHashCode() : 0);
				hashCode = (hashCode*397) ^ (value.LocalEndPoint != null ? value.LocalEndPoint.GetHashCode() : 0);
				return hashCode;
			}
		}
	}
}