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
    public class MessageController : ApiBaseController<IMessageService, Message, DtoMessage>
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService service) : base(service)
        {
            _messageService = service;
        }

        [HttpPost("SendMessage")]
        public IResponse<DtoMessage> SendMessage(DtoMessage item)
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _messageService.SendMessage(item, autUserId);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoMessage>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
