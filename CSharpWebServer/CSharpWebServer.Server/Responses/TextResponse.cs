namespace CSharpWebServer.Server.Responses
{
    using CSharpWebServer.Server.Http;
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            : base(text, HttpContentType.PlainText)
        {
        }
    }
}
