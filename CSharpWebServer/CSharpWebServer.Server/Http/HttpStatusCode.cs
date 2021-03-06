namespace CSharpWebServer.Server.Http
{
    public enum HttpStatusCode
    {
        OK = 200,
        Found = 302,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        TooLarge = 413,
        InternalServerError = 500
    }
}
