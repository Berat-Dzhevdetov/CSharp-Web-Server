namespace CSharpWebServer.Data
{
    using CSharpWebServer.Data.Models;
    using System.Collections.Generic;
    public class ApplicationDbContext : IData
    {
        public ApplicationDbContext()
        {
            this.Users = new HashSet<User>()
            {
                new User{
                    Id = 1,
                    Firstname = "Pesho",
                    Lastname = "Peshev",
                    Age = 15
                },
                new User{
                    Id = 2,
                    Firstname = "Gosho",
                    Lastname = "Goshev",
                    Age = 39
                },
                new User{
                    Id = 3,
                    Firstname = "Ivan",
                    Lastname = "Draganov",
                    Age = 24
                },
            };
        }

        public IEnumerable<User> Users { get; set; }
    }
}
