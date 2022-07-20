using ClientWithFluxor.Store.States;
using Fluxor;

namespace ClientWithFluxor.Store.Features.Counter;

public class CounterFeature : Feature<CounterState>
{
	public override string GetName() => "Counter";

	protected override CounterState GetInitialState() => new(0, false, null);
}
