using BusinessLogic.Abstract;
using Core;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Data;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Concrete
{
    public class AccountManager : IAccountService
    {
        private IUserDal _userDal;
        public AccountManager(IUserDal pUserDal)
        {
            _userDal = pUserDal;
        }

        public string Authenticate(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(ConfigSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(CustomClaimTypes.EMAIL, user.Email),
                    new Claim(CustomClaimTypes.ID, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(token);

            return tokenStr;
        }

        public User CheckUser(string email, string password)
        {
            User user = _userDal.Get(u => u.Email.Equals(email));
            
            if (user != null)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password))
                {
                    return user;
                }
            }

            return null;
        }
    }
}
