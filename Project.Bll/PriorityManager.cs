using Microsoft.Extensions.DependencyInjection;
using Project.Dal.Abstract;
using Project.Entity.Dto;
using Project.Entity.Models;
using Project.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Bll
{
    public class PriorityManager : GenericManager<Priority, DtoPriority>, IPriorityService
    {
        public readonly IPriorityRepository priorityRepository;
        public PriorityManager(IServiceProvider service) : base(service)
        {
            priorityRepository = service.GetService<IPriorityRepository>();
        }
    }
}
