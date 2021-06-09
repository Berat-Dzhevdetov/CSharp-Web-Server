namespace CSharpWebServer.Server.Responses
{
    using CSharpWebServer.Server.Http;
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html) : 
            base(html, HttpContentType.Html)
        {
        }
    }
}
