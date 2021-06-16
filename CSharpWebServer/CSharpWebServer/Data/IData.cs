namespace CSharpWebServer.Data
{
    using CSharpWebServer.Data.Models;
    using System.Collections.Generic;
    public interface IData
    {
        public IEnumerable<User> Users { get; }

    }
}
