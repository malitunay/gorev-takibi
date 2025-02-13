﻿using Project.Dal.Abstract;
using Project.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Dal.Concrete.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : EntityBase;
        bool BeginTransaction();
        bool RollBackTransaction();
        int SaveChanges();
    }
}
