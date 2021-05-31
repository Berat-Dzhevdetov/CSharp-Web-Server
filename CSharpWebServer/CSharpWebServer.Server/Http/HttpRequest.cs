namespace CSharpWebServer.Server.Http
{
    public class HttpRequest
    {
        public HttpMethod Method { get; private set; }
        public string Url { get; private set; }
        public HttpHeaderCollection Headers { get; } = new();

        public string Body { get; set; }
    }
}
