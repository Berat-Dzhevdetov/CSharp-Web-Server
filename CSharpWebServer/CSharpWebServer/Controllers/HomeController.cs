namespace CSharpWebServer.Controllers
{
    using CSharpWebServer.Server.Controllers;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Results;

    public class HomeController : Controller
    {

        public HomeController(HttpRequest request)
            : base(request)
        {
        }

        public ActionResult Index()
        {
            return Html("<h1>hi</h1>");
        }
        public ActionResult Cookie()
        {
            this.Response.AddCookie("Ivan", "15");
            this.Response.AddCookie("lastPage", "2");
            return Html("<h1>Your cookie has been set</h1>");
        }

        public HttpResponse StaticFiles()
        {
            return View();
        }
    }
}