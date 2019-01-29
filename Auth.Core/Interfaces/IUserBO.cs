using Auth.Core.DTO;
using System.Collections.Generic;

namespace Auth.Core.Interfaces
{
    public interface IUserBO
    {
        UserDTO Authenticate(string username, string password);
        IEnumerable<UserDTO> GetAll();
        UserDTO Get(int id);
    }
}
