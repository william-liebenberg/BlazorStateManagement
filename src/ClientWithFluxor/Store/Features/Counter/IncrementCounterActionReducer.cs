using ClientWithFluxor.Store.States;
using Fluxor;

namespace ClientWithFluxor.Store.Features.Counter;

public class IncrementCounterActionReducer : Reducer<CounterState, IncrementCounterAction>
{
	public override CounterState Reduce(CounterState state, IncrementCounterAction action)
	{
		return new CounterState(state.Count + action.StepSize, state.IsLoading, state.ErrorMessage);
	}
}
