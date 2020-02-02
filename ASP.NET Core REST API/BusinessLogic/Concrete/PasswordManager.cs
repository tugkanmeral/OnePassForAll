using BusinessLogic.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Concrete
{
    public class PasswordManager : IPasswordService
    {
        private IPassCardDal _passCardDal;

        public PasswordManager(IPassCardDal pPassCardDal)
        {
            _passCardDal = pPassCardDal;
        }

        public int AddPassword(PassCard passCard)
        {
            return _passCardDal.Add(passCard);
        }

        public void DeletePassword(PassCard passCard)
        {
            _passCardDal.Delete(passCard);
        }

        public PassCard GetPassword(int userId, int id)
        {
            return _passCardDal.Get(p => p.Id.Equals(id) && p.UserId.Equals(userId));
        }

        public IEnumerable<PassCard> GetPasswords(int userId)
        {
            return _passCardDal.GetList(p => p.UserId.Equals(userId));
        }

        public void UpdatePassword(PassCard passCard)
        {
            _passCardDal.Update(passCard);
        }
    }
}
