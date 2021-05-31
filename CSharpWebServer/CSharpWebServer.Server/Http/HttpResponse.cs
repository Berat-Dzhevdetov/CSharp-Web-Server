namespace CSharpWebServer.Server.Http
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; init; }

        public HttpHeaderCollection Headers { get; } = new();

        public string Content { get; set; }
    }
}
