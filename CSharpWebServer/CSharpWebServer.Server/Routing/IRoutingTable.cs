namespace CSharpWebServer.Server.Routing
{
    using System;
    using CSharpWebServer.Server.Http;

    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);
        IRoutingTable Map(HttpMethod method, string path, Func<HttpRequest, HttpResponse> responseFunc);
        IRoutingTable MapGet(string path, HttpResponse response);
        IRoutingTable MapGet(string path, Func<HttpRequest,HttpResponse> responseFunc);
        
        IRoutingTable MapPost(string path, HttpResponse response);
        IRoutingTable MapPost(string path, Func<HttpRequest, HttpResponse> responseFunc);
        
    }
}
