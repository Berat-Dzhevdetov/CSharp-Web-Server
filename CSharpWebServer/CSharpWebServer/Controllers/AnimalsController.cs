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

            if (!this.Request.Cookies.Contains(cookieName))
            {
                return new UnauthorizedResult(this.Response);
            }

            return View();
        }

        public ActionResult SetCookieToSeeCats()
        {
            this.Response.Cookies.Add("uid", "15");
            this.Response.Cookies.Add("lang", "en");
            return Redirect("/cats");
        }

        public ActionResult Dogs()
        {
            const string nameKey = "Name";
            const string Age = "Age";
            var query = Request.Query;
            var dogName = query.Contains(nameKey) ? query[nameKey] : "";
            var dogAge = query.Contains(Age) ? query[Age] : "";


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
