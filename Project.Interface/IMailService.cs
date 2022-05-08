using Project.Entity.Dto;
using Project.Entity.IBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interface
{
    public interface IMailService
    {
        Task<IResponse<DtoUser>> SendEmailAsync(DtoUser mailRequest);
    }
}
