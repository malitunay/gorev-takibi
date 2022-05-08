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
    public class PriorityController : ApiBaseController<IPriorityService, Priority, DtoPriority>
    {
        private readonly IPriorityService _priorityService;
        public PriorityController(IPriorityService service) : base(service)
        {
            _priorityService = service;
        }
    }
}
