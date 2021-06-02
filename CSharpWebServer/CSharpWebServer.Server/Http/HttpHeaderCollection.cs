namespace CSharpWebServer.Server.Http
{
    using System.Collections;
    using System.Collections.Generic;
    public class HttpHeaderCollection : IEnumerable<HttpHeader>
    {
        private readonly Dictionary<string, HttpHeader> headers;
        public int Count => this.headers.Count;

        public HttpHeaderCollection()
            => this.headers = new();

        public void Add(string name, string value)
        {
            var header = new HttpHeader(name, value);
            headers.Add(header.Name, header);
        }

        public IEnumerator<HttpHeader> GetEnumerator()
        => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        => this.GetEnumerator();
    }
}
