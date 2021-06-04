namespace CSharpWebServer.Server.Responses
{
    using CSharpWebServer.Server.Http;
    public class RedirectResponse : HttpResponse
    {
        public RedirectResponse() : base(HttpStatusCode.Redirect)
        {
        }
    }
}
