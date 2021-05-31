namespace CSharpWebServer.Server.Http
{
    using System.Collections.Generic;
    public class HttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;
        public HttpHeaderCollection()
            => this.headers = new();
    }
}
