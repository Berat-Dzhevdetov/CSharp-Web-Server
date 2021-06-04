namespace CSharpWebServer.Server.Responses
{
    public class HtmlResponse : ContentResponse
    {
        public HtmlResponse(string text) : 
            base(text, "text/html; charset=utf-8")
        {
        }
    }
}
