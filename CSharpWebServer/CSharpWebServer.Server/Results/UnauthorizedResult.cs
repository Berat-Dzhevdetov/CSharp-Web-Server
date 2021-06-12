namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;
    public class UnauthorizedResult : ActionResult
    {
        public UnauthorizedResult(HttpResponse response)
            : base(response)
        {
            this.StatusCode = HttpStatusCode.Unauthorized;
        }
    }
}
