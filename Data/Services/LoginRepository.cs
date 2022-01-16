using Data.Context;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services
{
    public class LoginRepository : ILoginRepository
    {
        private MyCmsContext _db;

        public LoginRepository(MyCmsContext _context)
        {
            _db = _context;
        }
        public bool IsExistUser(string username, string password)
        {
            return _db.AdminLogins.Any(u => u.UserName == username && u.Password == password);
        }
    }
}
