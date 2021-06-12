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
                    .MapGet<HomeController>("/", controller => controller.Index())
                    .MapGet<HomeController>("/cookie", controller => controller.Cookie())
                    .MapGet<AnimalsController>("/cats", controller => controller.Cats())
                    .MapGet<AnimalsController>("/secretCats", controller => controller.SetCookieToSeeCats())
                    .MapPost<AnimalsController>("/cats/save", controller => controller.Save())
                    .MapGet<AnimalsController>("/dogs", controller => controller.Dogs()));
            await server.Start();
        }
    }
}
