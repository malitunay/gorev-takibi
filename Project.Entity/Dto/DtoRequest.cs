using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoRequest : DtoBase
    {
        public DtoRequest()
        {
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
    }

    public partial class DtoNewRequest : DtoBase
    {
        public DtoNewRequest()
        {
        }

        public int Id { get; set; }
        public string RequestNo { get; set; }
        public string Subject { get; set; }
        public string DepartmentSubject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
        public int ReporterId { get; set; }
        public int DepartmentId { get; set; }
        public int PriorityId { get; set; }
        public List<DtoDepartment> Departments { get; set; }
    }


    public partial class DtoRequestDetail : DtoBase
    {
        public DtoRequest DtoRequest { get; set; }
        public List<DtoMessage> MessagesOfRequest { get; set; }
    }
}
