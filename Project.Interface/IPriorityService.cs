using Project.Entity.Dto;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interface
{
    public interface IPriorityService : IGenericService<Priority, DtoPriority>
    {
    }
}
