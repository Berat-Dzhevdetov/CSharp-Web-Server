namespace CSharpWebServer.Controllers
{
    using CSharpWebServer.Server.Controllers;
    using CSharpWebServer.Server.Http;

    public class HomeController : Controller
    {
        private readonly HttpRequest request;

        public HomeController(HttpRequest request)
            : base(request)
        {
        }

        public HttpResponse Index()
        {
            return Html("<h1>hi</h1>");
        }
    }
}