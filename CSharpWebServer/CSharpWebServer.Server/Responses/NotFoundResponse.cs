namespace CSharpWebServer.Server.Responses
{
    using CSharpWebServer.Server.Http;
    public class NotFoundResponse : HttpResponse
    {
        public NotFoundResponse() : base(HttpStatusCode.NotFound)
        {
        }
    }
}
