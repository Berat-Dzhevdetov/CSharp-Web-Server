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
            return null;
        }
        public HttpResponse Dogs()
        {
            const string nameKey = "Name";
            const string Age = "Age";
            var query = Request.Query;
            var dogName = query.ContainsKey(nameKey) ? query[nameKey] : "the dogs";
            var dogAge = query.ContainsKey(Age) ? query[Age] : "the dogs";


            return View(new { Name = dogName,Age = dogAge });
        }
        public HttpResponse Save()
        {
            var name = this.Request.Form["Name"];
            var age = this.Request.Form["Age"];

            return Text($"{name} the cat, {age} years old.");
        }
    }
}
