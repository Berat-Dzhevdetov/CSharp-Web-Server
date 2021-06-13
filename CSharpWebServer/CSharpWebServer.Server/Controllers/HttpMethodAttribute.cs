namespace CSharpWebServer.Server.Controllers
{
    using System;
    using CSharpWebServer.Server.Http;

    [AttributeUsage(AttributeTargets.Method)]
    public abstract class HttpMethodAttribute : Attribute
    {
        public HttpMethod HttpMethod { get; }

        protected HttpMethodAttribute(HttpMethod method)
            => this.HttpMethod = method;
    }
}
