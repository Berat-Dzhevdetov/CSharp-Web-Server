namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;
    public class HtmlResults : ContentResults
    {
        public HtmlResults(HttpResponse response, string html) : 
            base(response,html, HttpContentType.Html)
        {
        }
    }
}
