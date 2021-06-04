
namespace CSharpWebServer.Server.Http
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class HttpRequest
    {
        private const string NewLine = "\r\n";
        public  HttpMethod Method { get; private set; }
        public  string Path { get; private set; }
        public Dictionary<string,string> Query { get; private set; }
        public  HttpHeaderCollection Headers { get; private set; }

        public  string Body { get; set; }

        public static HttpRequest Parse(string request)
        {
            var lines = request.Split(NewLine);

            var startLine = lines.First().Split(" ");
            var method = ParseMethod(startLine[0]);
            var url = startLine[1];

            var (path,query) = ParseUrl(url);

            var headerLines = lines.Skip(1);
            var header = ParseHttpHeaders(headerLines);

            var bodyLines = lines.Skip(header.Count + 2).ToArray();
            var body = string.Join(NewLine,bodyLines);

            return new HttpRequest
            {
                Method = method,
                Path = path,
                Query = query,
                Headers = header,
                Body = body
            };
        }

        private static HttpMethod ParseMethod(string method)
        {
            try
            {
                return (HttpMethod)Enum.Parse(typeof(HttpMethod), method, true);
            }
            catch (Exception)
            {
                throw new InvalidOperationException($"Method '{method}' is not supported");
            }
        }
        private static (string, Dictionary<string,string>) ParseUrl(string url)
        {
            var urlParts = url.Split("?",2);
            var path = urlParts[0];
            var query = urlParts.Length > 1 ? ParseQuery(urlParts[1]) : new();
            return (path, query);
        }
        private static Dictionary<string,string> ParseQuery(string queryString)
            =>queryString
            .Split("&")
            .Select(part => part.Split("="))
            .Where(part => part.Length == 2)
            .ToDictionary(part => part[0], part => part[1]);

        private static HttpHeaderCollection ParseHttpHeaders(IEnumerable<string> headerLines)
        {
            var headerCollectoin = new HttpHeaderCollection();

            foreach (var headerLine in headerLines)
            {
                if (headerLine == string.Empty) break;

                var headerParts = headerLine.Split(":",2);

                if (headerParts.Length != 2)
                {
                    //TODO: Throw new BadRequestException
                    throw new InvalidOperationException("Request is not valid.");
                }

                headerCollectoin.Add(headerParts[0], headerParts[1].Trim());
            }

            return headerCollectoin;
        }
    }
}