namespace WebServer
{
    using System.Threading.Tasks;
    using CSharpWebServer.Server;
    using CSharpWebServer.Server.Responses;
    public class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:1234/
            var server = new HttpServer(
                    routes => routes
                    .MapGet("/", new HtmlResponse("<h1>Hello from the server!</h1>"))
                    .MapGet("/cats", new HtmlResponse("<h1>Hello from the cats</h1>")));
            await server.Start();
        }
    }
}
