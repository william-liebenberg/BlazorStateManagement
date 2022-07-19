using Fluxor;

namespace ClientWithFluxor.Store;

public abstract class FeatureClient : IFeatureClient
{
	private readonly ILogger _logger;
	private readonly IDispatcher _dispatcher;

	protected FeatureClient(ILogger logger, IDispatcher dispatcher)
		=> (_logger, _dispatcher) = (logger, dispatcher);

	public void Dispatch(object action) => _dispatcher.Dispatch(action);
}