namespace CSharpWebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string html) : 
            base(html, "text/html; charset=utf-8")
        {
        }
    }
}
