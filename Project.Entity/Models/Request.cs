using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Models
{
    public partial class Request : EntityBase
    {
        public Request()
        {
            Messages = new HashSet<Message>();
        }

        public int Id { get; set; }
        public string RequestNo { get; set; }
        public string Subject { get; set; }
        public string DepartmentSubject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int AssigneeId { get; set; }
        public int ReporterId { get; set; }
        public int DepartmentId { get; set; }
        public int PriorityId { get; set; }

        public virtual User Assignee { get; set; }
        public virtual Department Department { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual User Reporter { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
