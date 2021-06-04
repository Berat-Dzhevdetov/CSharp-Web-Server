namespace CSharpWebServer.Server.Responses
{
    public class TextResponse : ContentResponse
    {
        public TextResponse(string text)
            : base(text,"text/plain; charset=utf-8")
        {
        }
    }
}
