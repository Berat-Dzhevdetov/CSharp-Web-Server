namespace CSharpWebServer.Server.Controllers
{
    using System.Runtime.CompilerServices;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Identity;
    using CSharpWebServer.Server.Results;

    public abstract class Controller
    {
        private const string UserSessionKey = "AuthenticatedUsedId";
        protected HttpRequest Request { get; private init; }
        protected HttpResponse Response { get; private init; } = new(HttpStatusCode.OK);
        protected UserIdentity User { get; private set; }

        protected void SignIn(string uid)
        {
            this.Request.Session[UserSessionKey] = uid;
            this.User = new UserIdentity()
            {
                Id = uid
            };
        }

        protected void SignOut(string uid)
        {
            this.Request.Session.Remove(uid);
            this.User = new();
        }

        protected Controller(HttpRequest request)
        {
            this.Request = request;
            this.User = this.Request.Session.ContainsKey(UserSessionKey) ? new UserIdentity() { Id = this.Request.Session[UserSessionKey] } : new(); 
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