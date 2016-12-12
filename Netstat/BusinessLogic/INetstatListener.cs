namespace Netstat.BusinessLogic
{
	public interface INetstatListener
	{
		void OnChanged(TcpConnectionDescriptor descriptor);
	}
}