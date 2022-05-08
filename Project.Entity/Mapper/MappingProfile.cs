using AutoMapper;
using Project.Entity.Dto;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, DtoUser>().ReverseMap();
            CreateMap<Department, DtoDepartment>().ReverseMap();
            CreateMap<Message, DtoMessage>().ReverseMap();
            CreateMap<Priority, DtoPriority>().ReverseMap();
            CreateMap<Request, DtoRequest>().ReverseMap();
            CreateMap<Request, DtoNewRequest>().ReverseMap();
            CreateMap<Role, DtoRole>().ReverseMap();
            CreateMap<User, DtoLoginUser>();
            CreateMap<DtoLogin, User>();
        }
    }
}
