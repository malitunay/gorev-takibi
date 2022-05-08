using Microsoft.Extensions.DependencyInjection;
using Project.Dal.Abstract;
using Project.Entity.Dto;
using Project.Entity.Models;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll
{
    public class RoleeManager : GenericManager<Role, DtoRole>, IRoleService
    {
        public readonly IRoleRepository _roleRepository;
        public RoleeManager(IServiceProvider service) : base(service)
        {
            _roleRepository = service.GetService<IRoleRepository>();
        }
    }
}
