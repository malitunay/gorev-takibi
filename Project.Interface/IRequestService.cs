using Project.Entity.Dto;
using Project.Entity.IBase;
using Project.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Interface
{
    public interface IRequestService : IGenericService<Request, DtoRequest>
    {
        IResponse<DtoRequest> TakeRequest(int id, int autUserId, bool saveChanges = true);
        IResponse<DtoNewRequest> CreateRequest(DtoNewRequest item, bool saveChanges = true);
        IResponse<DtoRequestDetail> GetRequestDetail(int id, bool saveChanges = true);
        IResponse<List<DtoRequest>> GetListByUserId(int id, bool saveChanges = true);
        IResponse<List<DtoRequest>> GetListByDepartmentId(int id, bool saveChanges = true);
        IResponse<List<DtoRequest>> GetListByDepartmentOfUserId(int autUserId, bool saveChanges = true);
    }
}
