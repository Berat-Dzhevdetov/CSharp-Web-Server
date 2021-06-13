namespace CSharpWebServer.Server.Controllers
{
    using System.Runtime.CompilerServices;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Identity;
    using CSharpWebServer.Server.Results;

    public abstract class Controller
    {
        public const string UserSessionKey = "AuthenticatedUsedId";
        private UserIdentity userIdentity;
        protected HttpRequest Request { get; init; }
        protected HttpResponse Response { get; private init; } = new(HttpStatusCode.OK);
        protected UserIdentity User 
        {
            get
            {
                if(this.userIdentity == null)
                {
                    this.userIdentity = this.Request.Session.ContainsKey(UserSessionKey)
                        ? new UserIdentity { Id = this.Request.Session[UserSessionKey] }
                        : new();
                }
                return userIdentity;
            }
        }

        protected void SignIn(string uid)
        {
            this.Request.Session[UserSessionKey] = uid;
            this.userIdentity = new UserIdentity()
            {
                Id = uid
            };
        }

        protected void SignOut(string uid)
        {
            this.Request.Session.Remove(uid);
            this.userIdentity = new();
        }

        protected ActionResult Text(string text)
            => new TextResults(this.Response, text);

        protected ActionResult Html(string text)
            => new HtmlResults(this.Response, text);

        protected ActionResult Redirect(string location)
            => new RedirectResults(this.Response, location);

        protected ActionResult View([CallerMemberName] string viewName = "")
           => new ViewResults(this.Response,viewName, this.GetType().GetControllerName(),null);
        protected ActionResult View(object model, [CallerMemberName] string viewName = "")
           => new ViewResults(this.Response, viewName, this.GetType().GetControllerName(), model);
        protected ActionResult View(string viewName, object model)
           => new ViewResults(this.Response, viewName, this.GetType().GetControllerName(),model);

    }
}