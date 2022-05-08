using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Project.Entity.Dto;
using Project.Interface;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Project.Entity.IBase;
using Project.Entity.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Project.Bll
{
    public class MailManager : IMailService
    {
        private readonly DtoMailSettings _mailSettings;
        IConfiguration _configuration;
        public MailManager(IOptions<DtoMailSettings> mailSettings, IConfiguration configuration)
        {
            _mailSettings = mailSettings.Value;
            _configuration = configuration;
        }



        public async Task<IResponse<DtoUser>> SendEmailAsync(DtoUser mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
                email.To.Add(MailboxAddress.Parse(mailRequest.Email));
                email.Subject = "Yeni Personel Kaydı";
                var builder = new BodyBuilder();

                Random random = new Random();
                int randomNumber = random.Next(100000, 999999);

                byte[] data = Encoding.UTF8.GetBytes(randomNumber.ToString());
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_configuration["PasswordHash:Hash"]));
                    using (TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding=PaddingMode.PKCS7})
                    {
                        ICryptoTransform transform = tripleDES.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        mailRequest.Password = Convert.ToBase64String(results,0, results.Length);
                    }
                };


                builder.HtmlBody = "Your Password : " + randomNumber;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);

                var result = await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return new Response<DtoUser>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "Success",
                    Data = mailRequest
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





        //var email = new MimeMessage();
        //email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
        //email.To.Add(MailboxAddress.Parse(mailRequest.Email));
        //email.Subject = "Yeni Personel Kaydı";
        //var builder = new BodyBuilder();
        ////if (mailRequest.Attachments != null)
        ////{
        ////    byte[] fileBytes;
        ////    foreach (var file in mailRequest.Attachments)
        ////    {
        ////        if (file.Length > 0)
        ////        {
        ////            using (var ms = new MemoryStream())
        ////            {
        ////                file.CopyTo(ms);
        ////                fileBytes = ms.ToArray();
        ////            }
        ////            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
        ////        }
        ////    }
        ////}
        ////builder.HtmlBody = mailRequest.Body;
        //Random random = new Random();
        //int randomNumber = random.Next(100000, 999999);
        //builder.HtmlBody = "Your Password : " + randomNumber;
        //email.Body = builder.ToMessageBody();
        //using var smtp = new SmtpClient();
        //smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
        //smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //await smtp.SendAsync(email);
        //smtp.Disconnect(true);


    }
}
