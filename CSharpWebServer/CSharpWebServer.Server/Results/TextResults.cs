namespace CSharpWebServer.Server.Results
{
    using CSharpWebServer.Server.Http;
    public class TextResults : ContentResults
    {
        public TextResults(HttpResponse response,string text)
            : base(response,text, HttpContentType.PlainText)
        {
        }
    }
}
