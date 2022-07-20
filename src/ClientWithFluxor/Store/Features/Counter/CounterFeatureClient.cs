using Fluxor;

namespace ClientWithFluxor.Store.Features.Counter;

public class CounterFeatureClient : FeatureClient
{
	private readonly ILogger<CounterFeatureClient> _logger;

	public CounterFeatureClient(ILogger<CounterFeatureClient> logger, IDispatcher dispatcher) : base(logger, dispatcher)
	{
		_logger = logger;
	}

	public void Increment()
	{
		_logger.LogInformation("Incrementing Counter (default value)");

		Dispatch(new IncrementCounterAction());
	}

	public void IncrementBy(int count)
	{
		_logger.LogInformation("Incrementing Counter by " + count);

		Dispatch(new IncrementCounterAction()
		{
			StepSize = count
		});
	}
}
