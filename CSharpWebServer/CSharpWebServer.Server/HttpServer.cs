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
        private IPAddress ipAddress;
        private int port;
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

                var networkStream = connection.GetStream();

                var requestText = await this.ReadRequest(networkStream);
                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                var response = this.routingTable.ExecuteRequest(request);

                await this.WriteResponse(networkStream, response);

                connection.Close();
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
                    throw new NotImplementedException();
                }
                requestBuilder.AppendLine(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            } while (networkStream.DataAvailable);
            return requestBuilder.ToString().Trim();
        }
        private async Task WriteResponse(NetworkStream networkStream, HttpResponse response)
        {
            var responseBytes = Encoding.UTF8.GetBytes(response.ToString());

            await networkStream.WriteAsync(responseBytes);
        }
    }
}