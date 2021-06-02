namespace CSharpWebServer.Server.Responses
{
    using CSharpWebServer.Server.Common;
    using CSharpWebServer.Server.Http;
    using System.Text;
    public class TextResponse : HttpResponse
    {
        public TextResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            this.Headers.Add("Content-type", contentType);
            this.Headers.Add("Content-length", $"{Encoding.UTF8.GetByteCount(text)}");
            this.Content = text;
        }

        public TextResponse(string text)
            : this(text,"text/plain; charset=utf-8")
        {
        }
    }
}
