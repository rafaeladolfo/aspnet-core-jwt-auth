using Auth.Entities;
using System.Collections.Generic;

namespace Auth.Interfaces
{
    public interface IUserDAO
    {
        List<User> GetAll();
        User Get(int id);
    }
}
