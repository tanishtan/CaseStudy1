using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy1.DataAccess.Repositories
{
    public interface IRepository<TEntity, TIdentity>
    {
        IEnumerable<TEntity> GetAll();

        void AddNew(TEntity entity);
        TEntity FindById(TIdentity id);
        void Upsert(TEntity entity);
        void RemoveById(TIdentity id);
    }
}
