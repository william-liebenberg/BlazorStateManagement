using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared;

namespace SignalR_func
{
	public static class Function
	{
		private static readonly HttpClient HttpClient = new HttpClient();

		[FunctionName("index")]
		public static IActionResult Index([HttpTrigger(AuthorizationLevel.Anonymous)]HttpRequest req, ExecutionContext context)
		{
			var path = Path.Combine(context.FunctionAppDirectory, "content", "index.html");
			return new ContentResult
			{
				Content = File.ReadAllText(path),
				ContentType = "text/html",
			};
		}

		[FunctionName("negotiate")]
		public static SignalRConnectionInfo Negotiate(
			[HttpTrigger(AuthorizationLevel.Anonymous)] HttpRequest req,
			[SignalRConnectionInfo(HubName = "serverlessSample")] SignalRConnectionInfo connectionInfo)
		{
			return connectionInfo;
		}

		[FunctionName("QueueBoundFunction1")]
		public static async Task RunBound1(
			[TimerTrigger("*/1 * * * * *")] TimerInfo myTimer,
			[Queue("message-queue1")] QueueClient output,
			ILogger logger)
		{
			await Spam(output);
		}

		[FunctionName("QueueBoundFunction2")]
		public static async Task RunBound2(
			[TimerTrigger("*/1 * * * * *")] TimerInfo myTimer,
			[Queue("message-queue2")] QueueClient output,
			ILogger logger)
		{
			await Spam(output);
		}

		[FunctionName("QueueBoundFunction3")]
		public static async Task RunBound3(
			[TimerTrigger("*/1 * * * * *")] TimerInfo myTimer,
			[Queue("message-queue3")] QueueClient output,
			ILogger logger)
		{
			await Spam(output);
		}

		[FunctionName("QueueBoundFunction4")]
		public static async Task RunBound4(
			[TimerTrigger("*/1 * * * * *")] TimerInfo myTimer,
			[Queue("message-queue4")] QueueClient output,
			ILogger logger)
		{
			await Spam(output);
		}

		private static async Task Spam(QueueClient output)
		{
			var rnd = new Random();
			for (int j = 0; j < 16; j++)
			{
				var item = new ItemView()
				{
					Id = DateTime.Now.ToString("h:mm:ss fffffff tt zz"),
					Label = $"Blah {j}",
					RandomField01 = Guid.NewGuid().ToString(),
					RandomField02 = Guid.NewGuid().ToString(),
					RandomField03 = Guid.NewGuid().ToString(),
					RandomField04 = Guid.NewGuid().ToString(),
					RandomField05 = Guid.NewGuid().ToString(),
					RandomField06 = Guid.NewGuid().ToString(),
					RandomField07 = Guid.NewGuid().ToString(),
					RandomField08 = Guid.NewGuid().ToString(),
					RandomField09 = Guid.NewGuid().ToString(),
					RandomField10 = Guid.NewGuid().ToString(),
					RandomField11 = Guid.NewGuid().ToString(),
					RandomField12 = Guid.NewGuid().ToString()
				};

				var messageJson = JsonConvert.SerializeObject(item);
				await output.SendMessageAsync(messageJson, TimeSpan.FromMilliseconds(rnd.Next(10, 250)), TimeSpan.FromSeconds(30));
			}
		}

		[FunctionName("broadcastMessage1")]
		public static async Task BroadcastMessage1(
			[QueueTrigger("message-queue1")] string message,
			[SignalR(HubName = "serverlessSample")] IAsyncCollector<SignalRMessage> signalRMessages,
			ILogger logger)
		{
			await BroadcastViaSignalR(message, signalRMessages);
		}

		[FunctionName("broadcastMessage2")]
		public static async Task BroadcastMessage2(
			[QueueTrigger("message-queue2")] string message,
			[SignalR(HubName = "serverlessSample")] IAsyncCollector<SignalRMessage> signalRMessages,
			ILogger logger)
		{
			await BroadcastViaSignalR(message, signalRMessages);
		}

		[FunctionName("broadcastMessage3")]
		public static async Task BroadcastMessage3(
			[QueueTrigger("message-queue3")] string message,
			[SignalR(HubName = "serverlessSample")] IAsyncCollector<SignalRMessage> signalRMessages,
			ILogger logger)
		{
			await BroadcastViaSignalR(message, signalRMessages);
		}

		[FunctionName("broadcastMessage4")]
		public static async Task BroadcastMessage4(
			[QueueTrigger("message-queue4")] string message,
			[SignalR(HubName = "serverlessSample")] IAsyncCollector<SignalRMessage> signalRMessages,
			ILogger logger)
		{
			await BroadcastViaSignalR(message, signalRMessages);
		}

		private static async Task BroadcastViaSignalR(string message, IAsyncCollector<SignalRMessage> signalRMessages)
		{
			var item = JsonConvert.DeserializeObject<ItemView>(message);

			await signalRMessages.AddAsync(
				new SignalRMessage
				{
					Target = "NewItem",
					Arguments = new object[] { item }
				});
		}
	}
}