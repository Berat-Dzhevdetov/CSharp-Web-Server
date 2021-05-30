namespace WebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;
    public class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:1234/

            var address = IPAddress.Parse("127.0.0.1");
            var port = 4000;

            var serverListener = new TcpListener(address, port);

            serverListener.Start();

            Console.WriteLine($"Server started on port {port}...");
            Console.WriteLine("Awaiting for requests...");

            var connection = await serverListener.AcceptTcpClientAsync();

            var networkStream = connection.GetStream();

            var content = $"<h1>Hello from my server!</h1>";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = @$"HTTP/1.1 200 OK
Date: {DateTime.UtcNow:r}
Content-Length: {contentLength}
Content-Type: text/html;charset=utf-8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);

            connection.Close();
        }
    }
}
