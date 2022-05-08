using Microsoft.EntityFrameworkCore;
using Project.Dal.Abstract;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Concrete.EntityFramework.Repository
{
    public class RequestRepository : GenericRepository<Request>, IRequestRepository
    {
        public RequestRepository(DbContext context) : base(context)
        {

        }

        public List<Request> GetListByUserId(int id)
        {
            return dbset.Where(i=>i.AssigneeId == id).ToList();
        }

        public List<Request> GetListByDepartmentId(int id)
        {
            return dbset.Where(i => i.DepartmentId == id).ToList();
        }


        public Request TakeRequest(Request item, int autUserId)
        {
            // JWT işlemlerinden sonra authenticate olmuş kullanıcının id sini ver:
            
            item.AssigneeId = autUserId;
            dbset.Update(item);
            return item;
        }

        public Request CreateRequest(Request item)
        {
            dbset.Add(item);
            return item;

        }
    }
}
