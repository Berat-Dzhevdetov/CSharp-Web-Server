namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;
    public class NotFoundResults : ActionResult
    {
        public NotFoundResults(HttpResponse response) : base(response)
        {
            this.StatusCode = HttpStatusCode.NotFound;
        }
    }
}
