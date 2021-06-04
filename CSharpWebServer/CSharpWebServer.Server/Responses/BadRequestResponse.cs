namespace CSharpWebServer.Server.Responses
{
    using CSharpWebServer.Server.Http;
    public class BadRequestResponse : HttpResponse
    {
        public BadRequestResponse()
            : base(HttpStatusCode.BadRequest)
        {

        }
    }
}
