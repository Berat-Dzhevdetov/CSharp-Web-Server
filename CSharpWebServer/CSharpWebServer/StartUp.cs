namespace WebServer
{
    using System.Threading.Tasks;
    using CSharpWebServer.Controllers;
    using CSharpWebServer.Server;
    public class StartUp
    {
        public static async Task Main()
        {
            // http://localhost:1234/
            var server = new HttpServer(
                    routes => routes
                    .MapGet<HomeController>("/", controller => controller.Index())
                    .MapGet<AnimalsController>("/cats", controller => controller.Cats())
                    .MapGet<AnimalsController>("/dogs", controller => controller.Dogs()));
            await server.Start();
        }
    }
}
