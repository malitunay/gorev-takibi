using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interface
{
    public interface IUserService : IGenericService<User, DtoUser>
    {
        IResponse<DtoUserToken> Login(DtoLogin login);
        IResponse<DtoUser> AddUser(DtoUser user, bool saveChanges = true);
        IResponse<DtoRenewPassword> ChangePassword(DtoRenewPassword login, bool saveChanges = true);

    }
}
