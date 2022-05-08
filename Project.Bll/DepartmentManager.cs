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
    public class DepartmentManager : GenericManager<Department, DtoDepartment>, IDepartmentService
    {
        public readonly IDepartmentRepository departmentRepository;
        public DepartmentManager(IServiceProvider service) : base(service)
        {
            departmentRepository = service.GetService<IDepartmentRepository>();
        }
    }
}
