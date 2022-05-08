using Microsoft.EntityFrameworkCore;
using Project.Dal.Abstract;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Concrete.EntityFramework.Repository
{
    public class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(DbContext context) : base(context)
        {

        }

        public Message SendMessage(Message message)
        {
            dbset.Add(message);
            return message;
        }
    }
}
