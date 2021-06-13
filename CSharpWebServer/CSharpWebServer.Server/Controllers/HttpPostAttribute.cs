namespace CSharpWebServer.Server.Controllers
{
    using CSharpWebServer.Server.Http;
    class HttpPostAttribute : HttpMethodAttribute
    {
        public HttpPostAttribute()
            : base(HttpMethod.Post)
        {
        }
    }
}
