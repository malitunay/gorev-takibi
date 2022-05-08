using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Dal.Abstract;
using Project.Entity.Base;
using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using Project.Interface;
using Project.Bll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Project.Bll
{
    public class UserrManager : GenericManager<User, DtoUser>, IUserService
    {
        public readonly IUserRepository _userRepository;
        public readonly IRoleRepository _roleRepository;
        private readonly IMailService _mailService;
        private IConfiguration _configuration;
        public UserrManager(IServiceProvider service, IConfiguration configuration) : base(service)
        {
            _userRepository = service.GetService<IUserRepository>();
            _roleRepository = service.GetService<IRoleRepository>();
            _configuration = configuration;
        }

        public IResponse<DtoUserToken> Login(DtoLogin login)
        {
            try
            {
                // admin rolündeki bir kullanıcıyı veritabanına manuel olarak eklediğimiz için ve onun parolasını hash lemediğimiz için bu kontrolü yaparız:
                if (login.Email != "at@g.com")
                {
                    var encryptedLoginPass = new HashManager(_configuration).Encrypt(login.Password);
                    login.Password = encryptedLoginPass;
                }

                var user = ObjectMapper.Mapper.Map<DtoLoginUser>(_userRepository.Login(ObjectMapper.Mapper.Map<User>(login)));

                if (user != null)
                {
                    user.RoleName = _roleRepository.Find(user.RoleId).RoleName;
                    var dtoUser = ObjectMapper.Mapper.Map<DtoLoginUser>(user);
                    var token = new TokenManager(_configuration).CreateAccessToken(dtoUser);

                    var userToken = new DtoUserToken()
                    {
                        DtoLoginUser = dtoUser,
                        AccessToken = token
                    };

                    return new Response<DtoUserToken>
                    {
                        Message = "Token Üretildi.",
                        StatusCode = StatusCodes.Status200OK,
                        Data = userToken
                    };
                }
                else
                {
                    return new Response<DtoUserToken>
                    {
                        Message = "E-Posta veya parola yanlış",
                        StatusCode = StatusCodes.Status406NotAcceptable,
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                return new Response<DtoUserToken>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }




        public IResponse<DtoUser> AddUser(DtoUser item, bool saveChanges = true)
        {
            try
            {
                _mailService.SendEmailAsync(item);
                var model = ObjectMapper.Mapper.Map<User>(item);
                var result = _userRepository.Add(model);

                if (saveChanges)
                    Save();

                return new Response<DtoUser>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = ObjectMapper.Mapper.Map<User, DtoUser>(result)
                };

            }
            catch (Exception ex)
            {
                return new Response<DtoUser>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = $"Error:{ex.Message}",
                    Data = null
                };
            }
        }

        public IResponse<DtoRenewPassword> ChangePassword(DtoRenewPassword login, bool saveChanges = true)
        {
            var autUser = _userRepository.Find(login.Id);

            if (login.Email != "at@g.com")
            {
                var encryptedLoginPass = new HashManager(_configuration).Encrypt(login.OldPassword);
                login.OldPassword = encryptedLoginPass;
            }

            if (login.OldPassword != autUser.Password)
            {
                return new Response<DtoRenewPassword>
                {
                    Message = "Eski parolanız uyuşmuyor.",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
            }

            else
            {
                if (login.Email != "at@g.com")
                {
                    autUser.Password = new HashManager(_configuration).Encrypt(login.NewPassword);                    
                }
                else
                {
                    autUser.Password = login.NewPassword;
                }
                
                _userRepository.Update(autUser);

                if (saveChanges)                
                    Save();
                              
                return new Response<DtoRenewPassword>
                {
                    Message = "Parolanız başarılı bir şekilde güncellendi.",
                    StatusCode = StatusCodes.Status200OK,
                    Data = login
                };
            }

        }
    }
}
