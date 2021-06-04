namespace CSharpWebServer.Controllers
{
    using CSharpWebServer.Server;
    using CSharpWebServer.Server.Http;
    public class AnimalsController : Controller
    {
        private readonly HttpRequest request;

        public AnimalsController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            var query = request.Query;
            var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the cats";
            var result = $"<h1>Hello from {catName}</h1>";

            return Html(result);
        }
        public HttpResponse Dogs()
        {
            const string nameKey = "Name";
            var query = request.Query;
            var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the dogs";
            var result = $"<h1>Hello from {catName}</h1>";

            return Html(result);
        }
    }
}
