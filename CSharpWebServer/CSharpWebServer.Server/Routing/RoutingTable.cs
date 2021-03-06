
namespace CSharpWebServer.Server.Routing
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CSharpWebServer.Server.Common;
    using CSharpWebServer.Server.Http;
    using CSharpWebServer.Server.Settings;

    public class RoutingTable : IRoutingTable
    {
        private readonly Dictionary<HttpMethod, Dictionary<string, Func<HttpRequest,HttpResponse>>> routes;

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
            Guard.AgainstNull(response, nameof(response));
            this.Map(method, path, request => response);
            return this;
        }
        public IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunc)
        {
            Guard.AgainstNull(path, nameof(path));
            Guard.AgainstNull(responseFunc, nameof(responseFunc));
            this.routes[method][path.ToLower()] = responseFunc;
            return this;
        }

        public IRoutingTable MapGet(string path, HttpResponse response)
        => MapGet(path, request => response);

        public IRoutingTable MapGet(string path, Func<HttpRequest, HttpResponse> responseFunc)
        => Map(HttpMethod.Get, path, responseFunc);

        public IRoutingTable MapPost(string path, HttpResponse response)
        => MapPost(path, request => response);

        public IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunc)
        => Map(HttpMethod.Post, path, responseFunc);


        public IRoutingTable MapPut(string path, HttpResponse response)
        => Map(HttpMethod.Put, path, response);
        public IRoutingTable MapDelete(string path, HttpResponse response)
        => Map(HttpMethod.Delete, path, response);

        public HttpResponse ExecuteRequest(HttpRequest request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Path.ToLower();
            if (!this.routes.ContainsKey(requestMethod)
                || !this.routes[requestMethod].ContainsKey(requestUrl))
            {
                return new HttpResponse(HttpStatusCode.NotFound);
            }
            var responseFunc = this.routes[requestMethod][requestUrl];
            return responseFunc(request);
        }

        public IRoutingTable MapStaticFiles(string folder = Settings.StaticFilesRootFolder)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var staticFilesFolder = Path.Combine(currentDirectory, folder);
            var staticFiles = Directory.GetFiles(
                staticFilesFolder,
                "*.*",
                SearchOption.AllDirectories);

            foreach (var file in staticFiles)
            {
                var relativePath = Path.GetRelativePath(staticFilesFolder, file);

                var urlPath = "/" + relativePath.Replace("\\", "/");

                this.MapGet(urlPath, request =>
                {
                    var content = File.ReadAllBytes(file);
                    var fileExtension = Path.GetExtension(file).Trim('.');
                    var contentType = HttpContentType.GetByFileExtention(fileExtension);

                    return new HttpResponse(HttpStatusCode.OK)
                        .SetContent(content, contentType);
                });
            }

            return this;
        }
    }
}