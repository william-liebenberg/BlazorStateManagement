namespace ClientWithFluxor.Store.States;

public abstract class BaseState
{
	public BaseState(bool isLoading, string? errorMessage) =>
		(IsLoading, ErrorMessage) = (isLoading, errorMessage);

	public bool IsLoading { get; }
	public string? ErrorMessage { get; }
	public bool HasErrors => !string.IsNullOrWhiteSpace(ErrorMessage);
}
