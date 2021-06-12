namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;
    public class BadRequestResults : ActionResult
    {
        public BadRequestResults(HttpResponse response)
            : base(response)
        {
            this.StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
