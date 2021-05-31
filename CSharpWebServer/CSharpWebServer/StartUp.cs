namespace WebServer
{
    using CSharpWebServer.Server;
    using System.Threading.Tasks;
    public class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:1234/
            var ip = "127.0.0.1";
            var port = 4000;
            var server = new HttpServer(ip,port);
            await server.Start();
        }
    }
}
