using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Shared;

namespace BlazorGrid
{
	public sealed class DotnetSignalRService : ISignalRService, IAsyncDisposable
	{
		private readonly ILogger<DotnetSignalRService> _logger;
		private HubConnection? _connection;
		private bool _isInitialized;
		
		public DotnetSignalRService(ILogger<DotnetSignalRService> logger)
		{
			_logger = logger;
		}

		public async Task ConnectToSignalR(string hubUrl, CancellationToken ct = default)
		{
			if (_connection?.State is HubConnectionState.Connected or HubConnectionState.Connecting || _isInitialized == true)
			{
				return;
			}
			
			_logger.LogInformation("Connection to SignalR (.NET)");

			await Disconnect();

			_connection = new HubConnectionBuilder()
				.WithUrl(
					hubUrl,
					option =>
					{
						option.Transports =
							HttpTransportType.WebSockets |
							HttpTransportType.LongPolling |
							HttpTransportType.ServerSentEvents;
					})
				.WithAutomaticReconnect()
				.Build();

			await _connection.StartAsync(ct);

			_connection.Reconnecting += e =>
			{
				_logger.LogWarning(e, "SignalR reconnecting...");
				return Task.CompletedTask;
			};
			
			_connection.Reconnected += e =>
			{
				_logger.LogWarning("SignalR reconnected: " + e);
				return Task.CompletedTask;
			};

			_connection.Closed += e =>
			{
				_logger.LogWarning(e, "SignalR closed!");
				return Task.CompletedTask;
			};
			
			_logger.LogInformation("Connected to SignalR!");
		}

		public async Task Disconnect()
		{
			if (_connection != null)
			{
				_logger.LogInformation("Disconnecting...");
				await _connection.DisposeAsync();
				_connection = null;
				_isInitialized = false;
			}
		}

		public bool IsConnected() => _connection != null && _connection.State == HubConnectionState.Connected;

		public void SetHandler(ISignalRHandlerService handler)
		{
			if (_connection == null)
			{
				_logger.LogError("Initializing {className} failed! SignalR connection has not yet been established.", nameof(DotnetSignalRService));
				return;
			}

			if (_isInitialized)
			{
				_logger.LogWarning("SignalR messages are already configured!");
				return;
			}

			if (handler == null)
			{
				_logger.LogError("Requires SignalR handler!");
				return;
			}

			// Setup hooks
			_logger.LogInformation("Registering handlers...");
			_connection.On<ItemView>("NewItem", handler.OnNewItem);
			_connection.On<ItemView>("UpdateItem", handler.OnUpdateItem);

			_isInitialized = true;
		}

		public string GetConnectionId() => _connection?.ConnectionId ?? string.Empty;
		
		public async ValueTask DisposeAsync()
		{
			_logger.LogInformation("Disconnecting because Dispose!");
			await Disconnect();
		}
	}
}