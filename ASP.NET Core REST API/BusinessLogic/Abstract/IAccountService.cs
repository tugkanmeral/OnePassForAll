using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Abstract
{
    public interface IAccountService
    {
        User CheckUser(string email, string password);
        string Authenticate(User user);
    }
}
