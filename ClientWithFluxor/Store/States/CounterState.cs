namespace ClientWithFluxor.Store.States;

public class CounterState : BaseState
{
	public CounterState(int count, bool isLoading, string? errorMessage)
		: base(isLoading, errorMessage)
	{
		Count = count;
	}

	public int Count { get; }
}
