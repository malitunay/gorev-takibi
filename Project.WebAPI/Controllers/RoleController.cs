using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Entity.Dto;
using Project.Entity.Models;
using Project.Interface;
using Project.WebAPI.Base;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ApiBaseController<IRoleService, Role, DtoRole>
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService service) : base(service)
        {
            _roleService = service;
        }
    }
}
