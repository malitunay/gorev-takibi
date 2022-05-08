using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class DepartmentController : ApiBaseController<IDepartmentService, Department, DtoDepartment>
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService) : base(departmentService)
        {
            this._departmentService = departmentService;
        }
    }
}
