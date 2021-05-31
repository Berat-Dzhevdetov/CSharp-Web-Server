
namespace CSharpWebServer.Server
{
    using CSharpWebServer.Server.Http;
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

        public HttpServer(string ipAddress,int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.listener = new TcpListener(this.ipAddress,this.port);
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
                
                await this.WriteResponse(networkStream);

                connection.Close();
            }
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLenght = 1024;
            var buffer = new byte[bufferLenght];
            var totalBytesRead = 0;
            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLenght);

                totalBytesRead += bytesRead;

                if (totalBytesRead > 10 * 1024)
                {
                    //TODO: throw new TooLargeRequestException
                    throw new NotImplementedException();
                }
                requestBuilder.AppendLine(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }
            return requestBuilder.ToString();
        }
        private async Task WriteResponse(NetworkStream networkStream)
        {
            var content = $@"<!DOCTYPE html>
<html>
<head>
    <title>Home</title>
    <link rel=""icon"" href=""data:,"">
</head>
   

<body>
<h1>Heading of my server</h1>
</body>
   

</html>";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = @$"HTTP/1.1 200 OK
Server: MyServer
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html;charset=utf-8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);
        }
    }
}
