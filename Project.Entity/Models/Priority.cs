using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class Priority : EntityBase
    {
        public Priority()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Priority1 { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
