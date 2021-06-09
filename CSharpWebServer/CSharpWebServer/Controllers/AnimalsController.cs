namespace CSharpWebServer.Controllers
{
    using CSharpWebServer.Server.Controllers;
    using CSharpWebServer.Server.Http;
    public class AnimalsController : Controller
    {
        public AnimalsController(HttpRequest request)
            : base(request)
        {

        }

        public HttpResponse Cats()
        {
            const string nameKey = "Name";
            var query = Request.Query;
            var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the cats";
            var result = $"<h1>Hello from {catName}</h1>";

            return View();
        }
        public HttpResponse Dogs()
        {
            const string nameKey = "Name";
            var query = Request.Query;
            var catName = query.ContainsKey(nameKey) ? query[nameKey] : "the dogs";
            var result = $"<h1>Hello from {catName}</h1>";

            return View("Animals/Dogs");
        }
    }
}
