using Project.Entity.Base;
using System;
using System.Collections.Generic;

#nullable disable

namespace Project.Entity.Dto
{
    public partial class DtoDepartment : DtoBase
    {
        public DtoDepartment()
        {
        }

        public int Id { get; set; }
        public string Department1 { get; set; }

    }
}
