namespace CSharpWebServer.Server.Controllers
{
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
    }
}
