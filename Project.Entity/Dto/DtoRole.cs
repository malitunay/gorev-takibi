using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoRole : DtoBase
    {
        public DtoRole()
        {
            //Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }

        //public virtual ICollection<User> Users { get; set; }
    }
}
