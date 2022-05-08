using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class Department : EntityBase
    {
        public Department()
        {
            Requests = new HashSet<Request>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Department1 { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
