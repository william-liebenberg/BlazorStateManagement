using System.Threading;
using System.Threading.Tasks;

namespace BlazorGrid
{
	public interface ISignalRService
	{
		Task ConnectToSignalR(string hubUrl, CancellationToken ct = default);
		Task Disconnect();
		bool IsConnected();
		void SetHandler(ISignalRHandlerService handler);
		string GetConnectionId();
	}
}