
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

        public HttpServer(string ipAddress,int port, Action<IRoutingTable> routingTable)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            this.listener = new TcpListener(this.ipAddress,this.port);
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable) : this("127.0.0.1", port, routingTable)
        {
        }

        public HttpServer(Action<IRoutingTable> routingTable) : this(4000,routingTable)
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
                
                await this.WriteResponse(networkStream);

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
                if(bytesRead == 0) break;

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
        private async Task WriteResponse(NetworkStream networkStream)
        {
            var content = $@"<!DOCTYPE html>
<html>
<head>
    <title>Home</title>
    <link rel=""icon"" href=""data:,"">
</head>
<style>
* {{ box-sizing: border-box; }}

body {{
  margin: 0;
  font-family: Arial, Helvetica, sans-serif;
}}

.header {{
  overflow: hidden;
  background-color: #f1f1f1;
  padding: 20px 10px;
}}

.header a {{
  float: left;
  color: black;
  text-align: center;
  padding: 12px;
  text-decoration: none;
  font-size: 18px; 
  line-height: 25px;
  border-radius: 4px;
}}

.header a.logo {{
  font-size: 25px;
  font-weight: bold;
}}

.header a:hover {{
  background-color: #ddd;
  color: black;
}}

.header a.active {{
  background-color: dodgerblue;
  color: white;
}}

.header-right {{
  float: right;
}}

@media screen and (max-width: 500px) {{
  .header a {{
    float: none;
    display: block;
    text-align: left;
  }}
  
  .header-right {{
    float: none;
  }}
}}
</style>

<body>
<div class=""header"">
  <a href=""#default"" class=""logo"">CompanyLogo</a>
  <div class=""header-right"">
    <a class=""active"" href=""#home"">Home</a>
    <a href=""#contact"">Contact</a>
    <a href=""#about"">About</a>
  </div>
</div>
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
