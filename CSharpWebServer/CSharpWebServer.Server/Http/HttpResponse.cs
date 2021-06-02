namespace CSharpWebServer.Server.Http
{
    using System;
    using System.Text;
    public abstract class HttpResponse
    {
        public HttpResponse(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;

            this.Headers.Add("Server", "MyWebServer");
            this.Headers.Add("Date", $"{DateTime.UtcNow:r}");

        }

        public HttpStatusCode StatusCode { get; init; }

        public HttpHeaderCollection Headers { get; } = new();

        public string Content { get; set; }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}");
            foreach (var header in this.Headers)
            {
                result.AppendLine(header.ToString());
            }
            if (!string.IsNullOrEmpty(this.Content))
            {
                result.AppendLine();

                result.AppendLine(this.Content);
            }

            return result.ToString();
        }
    }
}
