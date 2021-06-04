
namespace CSharpWebServer.Server.Routing
{
    using CSharpWebServer.Server.Common;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Responses;
    using System;
    using System.Collections.Generic;

    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, HttpResponse>> routes;

        public RoutingTable()
        {
            this.routes = new()
            {
                [HttpMethod.Get] = new(),
                [HttpMethod.Post] = new(),
                [HttpMethod.Put] = new(),
                [HttpMethod.Delete] = new()
            };
        }
        public IRoutingTable Map(string url, HttpMethod method, HttpResponse response)
        => method switch
        {
            HttpMethod.Get => this.MapGet(url,response),
            _ => throw new InvalidOperationException($"Method '{method}' not supported")
        };

        public IRoutingTable MapGet(string url, HttpResponse response)
        {
            Guard.AgainstNull(url,nameof(url));
            Guard.AgainstNull(response, nameof(response));
            this.routes[HttpMethod.Get][url] = response;
            return this;
        }
        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;
            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }
            return this.routes[requestMethod][requestUrl];
        }
    }
}
