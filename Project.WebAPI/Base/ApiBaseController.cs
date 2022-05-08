using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Entity.Base;
using Project.Entity.IBase;
using Project.Interface;
using System;
using System.Collections.Generic;

namespace Project.WebAPI.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiBaseController<TService, T, TDto> : ControllerBase where TService : IGenericService<T, TDto> where T : EntityBase where TDto : DtoBase
    {
        private readonly TService service;
        public ApiBaseController(TService service)
        {
            this.service = service;
        }

        [HttpGet("Find")]
        public IResponse<TDto> Find(int id)
        {
            try
            {
                return service.Find(id);
            }
            catch (Exception ex)
            {
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [HttpPost("Add")]
        public IResponse<TDto> Add(TDto entity)
        {
            try
            {
                return service.Add(entity);
            }
            catch (Exception ex)
            {
                return new Response<TDto>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }


        [HttpGet("GetAll")]
        public IResponse<List<TDto>> GetAll(int id)
        {
            try
            {
                return service.GetAll();
            }
            catch (Exception ex)
            {
                return new Response<List<TDto>>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error : {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
