namespace CSharpWebServer.Controllers
{
    using CSharpWebServer.Server.Controllers;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Results;

    public class AnimalsController : Controller
    {
        public ActionResult Cats()
        {
            const string cookieName = "uid";

            if (!this.Request.Cookies.ContainsKey(cookieName))
            {
                return new UnauthorizedResult(this.Response);
            }

            return View();
        }

        public ActionResult SetCookieToSeeCats()
        {
            this.Response.AddCookie("uid", "15");
            this.Response.AddCookie("lang", "en");
            return Redirect("/cats");
        }

        public ActionResult Dogs()
        {
            const string nameKey = "Name";
            const string Age = "Age";
            var query = Request.Query;
            var dogName = query.ContainsKey(nameKey) ? query[nameKey] : "";
            var dogAge = query.ContainsKey(Age) ? query[Age] : "";


            return View(new { Name = dogName,Age = dogAge });
        }
        public ActionResult Save()
        {
            var name = this.Request.Form["Name"];
            var age = this.Request.Form["Age"];

            return Text($"{name} the cat, {age} years old.");
        }
    }
}
