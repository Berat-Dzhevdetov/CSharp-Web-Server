namespace CSharpWebServer.Server.Controllers
{
    using System.Runtime.CompilerServices;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Responses;

    public abstract class Controller
    {
        protected HttpRequest Request { get; private init; }

        protected Controller(HttpRequest request)
        => this.Request = request;

        protected HttpResponse Text(string text)
            => new TextResponse(text);

        protected HttpResponse Html(string text)
            => new HtmlResponse(text);

        protected HttpResponse Redirect(string location)
            => new RedirectResponse(location);

        protected HttpResponse View([CallerMemberName] string viewName = "")
           => new ViewResponse(viewName, GetNameOfController(),null);
        protected HttpResponse View(object model = null, [CallerMemberName] string viewName = "")
           => new ViewResponse(viewName, GetNameOfController(), model);
        protected HttpResponse View(string viewName, object model = null)
           => new ViewResponse(viewName, GetNameOfController(),model);

        private string GetNameOfController()
            => this.GetType().Name.Replace(nameof(Controller), string.Empty);
    }
}