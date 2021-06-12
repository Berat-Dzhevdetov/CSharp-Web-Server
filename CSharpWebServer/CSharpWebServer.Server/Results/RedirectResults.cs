namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;
    public class RedirectResults : ActionResult
    {
        public RedirectResults(HttpResponse response, string location) : base(response)
        {
            this.StatusCode = HttpStatusCode.Found;
            this.AddHeader(HttpHeader.Location,location);
        }
    }
}
