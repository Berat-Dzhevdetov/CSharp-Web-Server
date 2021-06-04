namespace CSharpWebServer.Server.Responses
{
    using CSharpWebServer.Server.Common;
    using CSharpWebServer.Server.Http;
    using System.Text;

    public class ContentResponse : HttpResponse
    {
        public ContentResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);

            this.Headers.Add("Content-type", contentType);
            this.Headers.Add("Content-length", $"{Encoding.UTF8.GetByteCount(text)}");
            this.Content = text;
        }
    }
}
