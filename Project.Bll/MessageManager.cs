using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Project.Dal.Abstract;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll
{
    public class MessageManager : GenericManager<Message, DtoMessage>, IMessageService
    {
        public readonly IMessageRepository messageRepository;
        public readonly IRequestRepository requestRepository;

        public MessageManager(IServiceProvider service) : base(service)
        {
            messageRepository = service.GetService<IMessageRepository>();
            requestRepository = service.GetService<IRequestRepository>();
        }

        public IResponse<DtoMessage> SendMessage(DtoMessage message, int autUserId, bool saveChanges = true)
        {
            try
            {
                var request = requestRepository.Find(message.RequestId);

                if (autUserId == request.AssigneeId || autUserId == request.ReporterId)
                {
                    

                    if (autUserId == request.AssigneeId)
                    {
                        message.SenderId = autUserId;
                        message.ReceiverId = request.ReporterId;
                    }
                    if (autUserId == request.ReporterId)
                    {
                        message.SenderId = autUserId;
                        message.ReceiverId = request.AssigneeId;
                    }

                    message.SendingDate = DateTime.Now;                    

                    var model = ObjectMapper.Mapper.Map<Message>(message);
                    messageRepository.SendMessage(model);

                    if (saveChanges)
                        Save();


                    return new Response<DtoMessage>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Success",
                        Data = message
                    };
                }

                else
                {
                    return new Response<DtoMessage>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Bu requestte assignee veya reporter olmadığınız için mesaj gönderemezsiniz.",
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new Response<DtoMessage>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }
    }
}
