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
    public class RequestManager : GenericManager<Request, DtoRequest>, IRequestService
    {
        public readonly IRequestRepository requestRepository;
        public readonly IUserRepository userRepository;
        public readonly IMessageRepository messageRepository;
        public RequestManager(IServiceProvider service) : base(service)
        {
            requestRepository = service.GetService<IRequestRepository>();
            userRepository = service.GetService<IUserRepository>();
            messageRepository = service.GetService<IMessageRepository>();
        }

        public IResponse<List<DtoRequest>> GetListByUserId(int id, bool saveChanges = true)
        {
            try
            {
                var list = requestRepository.GetListByUserId(id);
                var listDto = list.Select(x => ObjectMapper.Mapper.Map<DtoRequest>(x)).ToList();

                if (saveChanges)
                    Save();

                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = listDto
                };

            }
            catch (Exception ex)
            {
                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<List<DtoRequest>> GetListByDepartmentId(int id, bool saveChanges = true)
        {
            try
            {
                var list = requestRepository.GetListByDepartmentId(id);
                var listDto = list.Select(x => ObjectMapper.Mapper.Map<DtoRequest>(x)).ToList();

                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = listDto
                };

            }
            catch (Exception ex)
            {
                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<List<DtoRequest>> GetListByDepartmentOfUserId(int autUserId, bool saveChanges = true)
        {
            try
            {
                var departmentId = userRepository.Find(autUserId).DepartmentId;
                var list = requestRepository.GetListByDepartmentId(departmentId);
                var listDto = list.Select(x => ObjectMapper.Mapper.Map<DtoRequest>(x)).ToList();

                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = listDto
                };

            }
            catch (Exception ex)
            {
                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<DtoRequest> TakeRequest(int id, int autUserId, bool saveChanges = true)
        {
            try
            {
                var model = requestRepository.Find(id);
                
                // Sadece unassigned olan requestlerin assignee edilebilmesi için kontrol:
                if (model.AssigneeId == 4)
                {
                    var result = requestRepository.TakeRequest(model, autUserId);
                    if (saveChanges)
                        Save();

                    return new Response<DtoRequest>
                    {
                        StatusCode = StatusCodes.Status200OK,
                        Message = "Success",
                        Data = ObjectMapper.Mapper.Map<Request, DtoRequest>(requestRepository.TakeRequest(model, autUserId))
                    };
                }

                // assigne edilmiş bir iş başka biri tarafından alınmak istenirse:
                else
                {
                    return new Response<DtoRequest>
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Request have already been assigneed",
                        Data = ObjectMapper.Mapper.Map<Request, DtoRequest>(requestRepository.TakeRequest(model, autUserId))
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response<DtoRequest>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<DtoNewRequest> CreateRequest(DtoNewRequest item, bool saveChanges = true)
        {
            try
            {
                var model = ObjectMapper.Mapper.Map<Request>(item);
                model.AssigneeId = 4;
                requestRepository.Add(model);

                if (saveChanges)
                    Save();

                return new Response<DtoNewRequest>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = item
                };

            }
            catch (Exception ex)
            {
                return new Response<DtoNewRequest>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<DtoRequestDetail> GetRequestDetail(int id, bool saveChanges = true)
        {
            try
            {

                var requestDetail = requestRepository.Find(id);
                var messagesOfRequest = messageRepository.GetAll(i=>i.RequestId == id).ToList();

                DtoRequestDetail detailsOfRequest = new DtoRequestDetail();

                detailsOfRequest.DtoRequest = ObjectMapper.Mapper.Map<DtoRequest>(requestDetail);
                detailsOfRequest.MessagesOfRequest = ObjectMapper.Mapper.Map<List<DtoMessage>>(messagesOfRequest);

                return new Response<DtoRequestDetail>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = detailsOfRequest
                };

            }
            catch (Exception ex)
            {
                return new Response<DtoRequestDetail>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }
    }
}
