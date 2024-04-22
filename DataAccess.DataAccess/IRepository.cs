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

    public interface IRepositoryAsync<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task CreateNew(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(int id);
    }
}
