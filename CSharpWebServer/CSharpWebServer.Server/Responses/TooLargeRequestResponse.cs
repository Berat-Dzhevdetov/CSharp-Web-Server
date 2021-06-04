using CSharpWebServer.Server.Http;

namespace CSharpWebServer.Server.Responses
{
    public class TooLargeRequestResponse : HttpResponse
    {
        public TooLargeRequestResponse() : base(HttpStatusCode.TooLarge)
        {
        }
    }
}
