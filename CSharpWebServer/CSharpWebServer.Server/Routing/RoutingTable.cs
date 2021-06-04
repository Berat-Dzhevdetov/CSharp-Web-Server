
namespace CSharpWebServer.Server.Routing
{
    using CSharpWebServer.Server.Common;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Responses;
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
        public IRoutingTable Map(HttpMethod method, string path, HttpResponse response)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(response, nameof(response));
            this.routes[method][path] = response;
            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
        => Map(HttpMethod.Get, path, response);
        public IRoutingTable MapPost(string path, HttpResponse response)
        => Map(HttpMethod.Post, path, response);
        public IRoutingTable MapPut(string path, HttpResponse response)
        => Map(HttpMethod.Put, path, response);
        public IRoutingTable MapDelete(string path, HttpResponse response)
        => Map(HttpMethod.Delete, path, response);

        public HttpResponse MatchRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Path;
            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }
            return this.routes[requestMethod][requestUrl];
        }
    }
}