namespace CSharpWebServer.Server.Http
{
    public enum HttpStatusCode
    {
        OK = 200,
        Redirect = 302,
        BadRequest = 400,
        NotFound = 404,
        TooLarge = 413
    }
}
