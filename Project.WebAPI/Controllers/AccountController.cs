using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Interface;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        IConfiguration _configuration;
        public AccountController(IUserService userService, IConfiguration configuration) 
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public IResponse<DtoUserToken> Login(DtoLogin login)
        {
            try
            {
                return _userService.Login(login);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoUserToken>
                {
                    Message = "Error : " + ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null
                };
            }
        }

    }
}
