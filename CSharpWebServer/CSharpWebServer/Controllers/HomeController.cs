namespace CSharpWebServer.Controllers
{
    using CSharpWebServer.Server.Controllers;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Results;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public HttpResponse StaticFiles()
        {
            return View();
        }
    }
}