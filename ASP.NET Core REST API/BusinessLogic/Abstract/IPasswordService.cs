using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Abstract
{
    public interface IPasswordService
    {
        IEnumerable<PassCard> GetPasswords(int userId);
        PassCard GetPassword(int userId, int id);
        int AddPassword(PassCard passCard);
        void UpdatePassword(PassCard passCard);
        void DeletePassword(PassCard passCard);
    }
}
