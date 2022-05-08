using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoUser : DtoBase
    {
        public DtoUser()
        {
            //MessageReceivers = new HashSet<Message>();
            //MessageSenders = new HashSet<Message>();
            //RequestAssignees = new HashSet<Request>();
            //RequestReporters = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public string Telephone { get; set; }

        //public virtual Department Department { get; set; }
        //public virtual Role Role { get; set; }
        //public virtual ICollection<Message> MessageReceivers { get; set; }
        //public virtual ICollection<Message> MessageSenders { get; set; }
        //public virtual ICollection<Request> RequestAssignees { get; set; }
        //public virtual ICollection<Request> RequestReporters { get; set; }
    }
}
