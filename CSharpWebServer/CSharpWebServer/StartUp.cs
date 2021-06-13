namespace WebServer
{
    using System.Threading.Tasks;
    using CSharpWebServer.Server;
    using CSharpWebServer.Controllers;
    using CSharpWebServer.Server.Controllers;
    public class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:1234/
            var server = new HttpServer(
                    routes => routes
                    .MapStaticFiles()
                    .MapControllers());
            await server.Start();
        }
    }
}
