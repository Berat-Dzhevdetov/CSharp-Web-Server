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
                    .MapGet("/cats", request =>
                    {
                        const string nameKey = "Name";
                        var query = request.Query;
                        var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the cats";
                        var result = $"<h1>Hello from {catName}</h1>";

                        return new HtmlResponse(result);
                    }));
            await server.Start();
        }
    }
}
