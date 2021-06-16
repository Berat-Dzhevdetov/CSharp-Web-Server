namespace CSharpWebServer.Controllers
{
    using CSharpWebServer.Data;
    using CSharpWebServer.Server.Controllers;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Results;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly IData data;

        public HomeController(IData data)
        {
            this.data = data;
        }

        public ActionResult Index()
        {
            return View();
        }
        public HttpResponse StaticFiles()
        {
            return View();
        }
        public HttpResponse All()
        {
            var users = data.Users.ToList();
            return View(users);
        }
    }
}