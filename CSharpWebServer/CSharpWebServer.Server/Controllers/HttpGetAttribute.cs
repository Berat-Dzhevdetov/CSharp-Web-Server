namespace CSharpWebServer.Server.Controllers
{
    using CSharpWebServer.Server.Http;
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public HttpGetAttribute()
            : base(HttpMethod.Get)
        {

        }
    }
}
