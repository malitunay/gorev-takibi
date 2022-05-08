using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interface
{
    public interface IMessageService : IGenericService<Message, DtoMessage>
    {
        IResponse<DtoMessage> SendMessage(DtoMessage message, int autUserId, bool saveChanges = true);
    }
}
