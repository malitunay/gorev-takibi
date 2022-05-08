using AutoMapper.Internal.Mappers;
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
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ApiBaseController<IRequestService, Request, DtoRequest>
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService service) : base(service)
        {
            _requestService = service;
        }

        // assign edilmemiş (assigneeId = 4) olan requesti, login olan user kendi üzerine alabilir 
        [HttpPut("TakeRequest")]
        public IResponse<DtoRequest> TakeRequest(int id)
        {
            try
            {
                // requesti üzerine almak isteyen authenticated user ın id değerini yakalama:
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _requestService.TakeRequest(id, autUserId);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoRequest>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        // Belli bir user'a assign edilmiş requestleri getirir
        [Authorize(Roles = "Admin, Yönetici")]
        [HttpGet("GetOwnListByAssigneeId")]
        public IResponse<List<DtoRequest>> GetListByUserId(int id)
        {
            try
            {
                return _requestService.GetListByUserId(id);               
            }
            catch (System.Exception ex)
            {
                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        // Belli bir departmana açılmış tüm requestleri getirir (Yönetici ve admin görebilir)
        [Authorize(Roles = "Admin, Yönetici")]
        [HttpGet("GetListByDepartmentId")]
        public IResponse<List<DtoRequest>> GetListByDepartmentId(int id)
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _requestService.GetListByDepartmentId(id);
            }
            catch (System.Exception ex)
            {
                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        // Login olan personelin bağlı olduğu departmana açılmış requestleri getirir (personel de görebilir)
        [HttpGet("GetListByDepartmentOfUserId")]
        [Authorize(Roles = "Admin, Yönetici, Personel")]
        public IResponse<List<DtoRequest>> GetListByDepartmentOfUserId()
        {
            try
            {
                var autUserId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);
                return _requestService.GetListByDepartmentOfUserId(autUserId);
            }
            catch (System.Exception ex)
            {
                return new Response<List<DtoRequest>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        [Authorize(Roles = "Admin, Yönetici, Personel")]
        [HttpPost("CreateRequest")]
        public IResponse<DtoNewRequest> CreateRequest(DtoNewRequest item)
        {
            try
            {
                item.ReporterId = Int32.Parse(User.Claims.First(i => i.Type == "jti").Value);               
                return _requestService.CreateRequest(item);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoNewRequest>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

        [Authorize(Roles = "Admin, Yönetici, Personel")]
        [HttpGet("GetRequestDetail")]
        public IResponse<DtoRequestDetail> GetRequestDetail(int id)
        {
            try
            {
                return _requestService.GetRequestDetail(id);
            }
            catch (System.Exception ex)
            {
                return new Response<DtoRequestDetail>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }

    }
}
