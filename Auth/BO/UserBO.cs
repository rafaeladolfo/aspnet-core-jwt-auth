using Auth.Core.DTO;
using Auth.Core.Interfaces;
using Auth.DAO;
using Auth.Helpers;
using Auth.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Auth.BO
{
    public class UserBO : IUserBO
    {
        private readonly IUserDAO _userDAO;
        private readonly AppSettings _appSettings;

        public UserBO(IOptions<AppSettings> appSettings, IUserDAO userDAO)
        {
            _appSettings = appSettings.Value;
            _userDAO = userDAO;
        }

        public UserDTO Authenticate(string login, string password)
        {                                    
            var _users = Mapper.Map<List<UserDTO>>(_userDAO.GetAll());
            var user = _users.SingleOrDefault(a => a.Login == login && a.Password == password);
            
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);            
            user.Password = null;

            return user;
        }

        public IEnumerable<UserDTO> GetAll()
        {            
            var users = Mapper.Map<List<UserDTO>>(_userDAO.GetAll());
            return users.Select(a => {
                a.Password = null;
                return a;
            });
        }

        public UserDTO Get(int id)
        {
            var user = Mapper.Map<UserDTO>(_userDAO.Get(id));            

            if (user != null)
                user.Password = null;

            return user;            
        }
    }
}
