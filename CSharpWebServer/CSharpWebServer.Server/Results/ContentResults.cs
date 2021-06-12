namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;

    public class ContentResults : ActionResult
    {
        public ContentResults(HttpResponse response,string content, string contentType)
            : base(response)
        => this.PrepareContent(content, contentType);
    }
}
