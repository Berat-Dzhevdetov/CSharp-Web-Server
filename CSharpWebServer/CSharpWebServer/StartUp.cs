namespace WebServer
{
    using CSharpWebServer.Data;
    using CSharpWebServer.Server;
    using CSharpWebServer.Server.Controllers;
    using System.Threading.Tasks;
    public class StartUp
    {
        // http://localhost:1234/
        public static async Task Main()
        => await HttpServer
            .WithRoutes(routes => routes
                .MapStaticFiles()
                .MapControllers())
            .WithServices(services => services
                .Add<IData, ApplicationDbContext>())
            .Start();
    }
}
