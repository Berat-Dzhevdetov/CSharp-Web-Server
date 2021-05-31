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

            while (true)
            {
                var connection = await serverListener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();

                var bufferLenght = 1024;
                var buffer = new byte[bufferLenght];
                var totalBytesRead = 0;
                var requestBuilder = new StringBuilder();
                while (networkStream.DataAvailable)
                {
                    var bytesRead = await networkStream.ReadAsync(buffer,0,bufferLenght);

                    totalBytesRead += bytesRead;

                    if (totalBytesRead > 10 * 1024)
                    {
                        //TODO: Implement HTTP status code 431
                        connection.Close();
                    }
                    requestBuilder.AppendLine(Encoding.UTF8.GetString(buffer,0,bytesRead));
                }
                Console.WriteLine(requestBuilder.ToString());

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

                connection.Close();
            }
        }
    }
}
