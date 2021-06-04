namespace WebServer
{
    using CSharpWebServer.Server;
    using CSharpWebServer.Server.Responses;
    using System.Threading.Tasks;
    public class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:1234/
            var server = new HttpServer(
                    routes => routes
                    .MapGet("/", new TextResponse("Hello from the server!"))
                    .MapGet("/cats", new TextResponse("Hello from the cats")));
            await server.Start();
        }
    }
}
