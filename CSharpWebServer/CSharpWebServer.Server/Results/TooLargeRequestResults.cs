namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;
    public class TooLargeRequestResults : ActionResult
    {
        public TooLargeRequestResults(HttpResponse response) : base(response)
        {
            this.StatusCode = HttpStatusCode.TooLarge;
        }
    }
}
