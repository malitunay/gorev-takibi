using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class Message : EntityBase
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Messagge { get; set; }
        public int RequestId { get; set; }
        public DateTime? SendingDate { get; set; }

        public virtual User Receiver { get; set; }
        public virtual Request Request { get; set; }
        public virtual User Sender { get; set; }
    }
}
