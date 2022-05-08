using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Abstract
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Request TakeRequest(Request item, int autUserId);
        Request CreateRequest(Request item);
        List<Request> GetListByUserId(int id);
        List<Request> GetListByDepartmentId(int id);
    }
}
