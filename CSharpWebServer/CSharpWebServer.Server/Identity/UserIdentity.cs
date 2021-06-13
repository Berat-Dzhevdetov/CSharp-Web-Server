namespace CSharpWebServer.Server.Identity
{
    public class UserIdentity
    {
        public string Id { get; set; }
        public bool IsAuthenticated => Id != null;
    }
}
