namespace CSharpWebServer.Server.Controllers
{
    using System.Runtime.CompilerServices;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Results;

    public abstract class Controller
    {
        protected HttpRequest Request { get; private init; }
        protected HttpResponse Response { get; private init; }

        protected Controller(HttpRequest request)
        {
            this.Request = request;
            this.Response = new HttpResponse(HttpStatusCode.OK);
        }

        protected ActionResult Text(string text)
            => new TextResults(this.Response, text);

        protected ActionResult Html(string text)
            => new HtmlResults(this.Response, text);

        protected ActionResult Redirect(string location)
            => new RedirectResults(this.Response, location);

        protected ActionResult View([CallerMemberName] string viewName = "")
           => new ViewResults(this.Response,viewName, GetNameOfController(),null);
        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
           => new ViewResults(this.Response, viewName, GetNameOfController(), model);
        protected ActionResult View(string viewName, object model)
           => new ViewResults(this.Response, viewName, GetNameOfController(),model);

        private string GetNameOfController()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}