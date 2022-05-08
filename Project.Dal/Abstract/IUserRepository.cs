using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Abstract
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User Login(User login);
    }
}
