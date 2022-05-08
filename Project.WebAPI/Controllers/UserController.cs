using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using Project.Interface;
using Project.WebAPI.Base;
using System;
using System.Linq;

namespace Project.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController<IUserService, User, DtoUser>
    {
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        public UserController(IUserService userService, IMailService mailService) : base(userService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        [Authorize(Roles = "Admin, Yönetici")]
        [HttpPost("AddUser")]
        public IResponse<DtoUser> AddUser(DtoUser entity)
        {
            try
            {
                _mailService.SendEmailAsync(entity);
                return _userService.Add(entity);
            }
            catch (Exception ex)
            {
                return new Response<DtoUser>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [Authorize(Roles = "Admin, Yönetici, Personel")]
        [HttpPost("ChangePassword")]
        public IResponse<DtoRenewPassword> ChangePassword(DtoRenewPassword login)
        {
            try
            {
                login.Id = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _userService.ChangePassword(login);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoRenewPassword>
                {
                    Message = "Error : " + ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Data = null
                };
            }
        }




    }
}
