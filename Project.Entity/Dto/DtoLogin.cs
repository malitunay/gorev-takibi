using Project.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entity.Dto
{
    public class DtoLogin : DtoBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
