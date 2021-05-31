namespace CSharpWebServer.Server.Http
{
    using System.Collections.Generic;
    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;
        public int Count => this.headers.Count;

        public HttpHeaderCollection()
            => this.headers = new();

        public void Add(HttpHeader header) => headers.Add(header.Name,header);
    }
}
