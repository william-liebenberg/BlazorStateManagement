using System.Threading.Tasks;
using Shared;

namespace BlazorGrid
{
	public interface ISignalRHandlerService
	{
		Task OnNewItem(ItemView item);
		Task OnUpdateItem(ItemView obj);
	}
}