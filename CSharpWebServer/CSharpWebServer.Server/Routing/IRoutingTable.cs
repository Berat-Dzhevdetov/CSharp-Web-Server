namespace CSharpWebServer.Server.Routing
{
    using CSharpWebServer.Server.Http;
    public interface IRoutingTable
    {
        IRoutingTable Map(HttpMethod method, string path, HttpResponse response);
        IRoutingTable MapGet(string path, HttpResponse response);
    }
}
