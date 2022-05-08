using Microsoft.EntityFrameworkCore;
using Project.Dal.Abstract;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Concrete.EntityFramework.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public User Login(User login)
        {

            return dbset.Where(i=>i.Email == login.Email && i.Password == login.Password).SingleOrDefault();
        }
    }
}
