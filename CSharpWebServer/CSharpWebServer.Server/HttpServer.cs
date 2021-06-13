namespace CSharpWebServer.Server
{
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Routing;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;
        private readonly RoutingTable routingTable;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTableConfiguration)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.listener = new TcpListener(this.ipAddress, this.port);
            this.routingTable = new();
            routingTableConfiguration(routingTable);
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable) : this("127.0.0.1", port, routingTable)
        {
        }

        public HttpServer(Action<IRoutingTable> routingTable) : this(4000, routingTable)
        {
        }

        public async Task Start()
        {
            listener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Awaiting for requests...");

            while (true)
            {
                var connection = await listener.AcceptTcpClientAsync();
                _ = Task.Run(async () =>
                {
                    var networkStream = connection.GetStream();
                    var requestText = await this.ReadRequest(networkStream);

                    try
                    {
                        var request = HttpRequest.Parse(requestText);

                        var response = this.routingTable.ExecuteRequest(request);

                        this.PrepareSession(response, request);

                        this.LogPipeLine(request, response.ToString());

                        await this.WriteResponse(networkStream, response);
                    }
                    catch (Exception ex)
                    {
                        await HandleError(ex, networkStream);
                    }

                    connection.Close();
                });
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];
            var totalBytesRead = 0;
            var requestBuilder = new StringBuilder();

            do
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                if (bytesRead == 0) break;

                totalBytesRead += bytesRead;

                if (totalBytesRead > 10 * 1024)
                {
                    //TODO: throw new TooLargeRequestException
                    throw new TimeoutException();
                }
                requestBuilder.AppendLine(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            } while (networkStream.DataAvailable);
            return requestBuilder.ToString().Trim();
        }

        private async Task HandleError(Exception ex, NetworkStream networkStream)
        {
            var errorMsg = $"{ex.Message} {Environment.NewLine} {ex.StackTrace}";
            var errorResponse = HttpResponse.ForError(errorMsg);
            await this.WriteResponse(networkStream, errorResponse);
        }

        private void LogPipeLine(HttpRequest request, string response)
        {
            var separator = new string('-', 50);

            var log = new StringBuilder();

            log.AppendLine();
            log.AppendLine(separator);
            log.AppendLine("Request:");
            log.AppendLine(request.ToString());
            log.AppendLine();
            log.AppendLine("Response:");
            log.AppendLine(response);

            Console.WriteLine(log.ToString());
        }

        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes);
        }

        private void PrepareSession(HttpResponse response, HttpRequest request)
        {
            if (request.Session.IsNew)
            {
                response.AddCookie(HttpSession.SessionCookieName, request.Session.Id);
                request.Session.IsNew = false;
            }
        }
    }
}