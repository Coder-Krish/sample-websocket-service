using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace MyWebSocketService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpListener _listener;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:8085/");
            _listener.Start();

            _logger.LogInformation("WebSocket server started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    HttpListenerWebSocketContext webSocketContext = await context.AcceptWebSocketAsync(null);
                    WebSocket webSocket = webSocketContext.WebSocket;

                    _logger.LogInformation("WebSocket connection established");

                    await HandleWebSocketConnection(webSocket, stoppingToken);
                }
            }

            _listener.Stop();
        }

        private async Task HandleWebSocketConnection(WebSocket webSocket, CancellationToken stoppingToken)
        {
            var buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open && !stoppingToken.IsCancellationRequested)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), stoppingToken);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    _logger.LogInformation($"Received: {message}");

                    string response = $"Server received: {message}";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    await webSocket.SendAsync(new ArraySegment<byte>(responseBytes), WebSocketMessageType.Text, true, stoppingToken);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, stoppingToken);
                }
            }

            _logger.LogInformation("WebSocket connection closed");
        }
    }
}