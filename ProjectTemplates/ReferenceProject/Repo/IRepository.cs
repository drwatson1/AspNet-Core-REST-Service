using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReferenceProject.Repo
{
    public interface IRepository<T> where T : class, Model.IEntity
    {
        void Create(T e);
        void Delete(int id);
        IQueryable<T> Get();
        void Update(T e);
    }
}
