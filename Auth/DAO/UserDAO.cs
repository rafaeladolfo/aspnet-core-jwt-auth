using Auth.Entities;
using Auth.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Auth.DAO
{
    public class UserDAO : IUserDAO
    {
        public List<User> GetAll()
        {
            var user = new List<User>
            {
                new User { Id = 1, FirstName = "Admin", LastName = "Master", Login = "admin", Password = "admin" },
                new User { Id = 2, FirstName = "Regular", LastName = "User", Login = "user", Password = "user" }
            };
            return user;
        }

        public User Get(int id)
        {
            var users = GetAll();
            return users.FirstOrDefault(a => a.Id.Equals(id));
        }
    }
}
