using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public class RoleImplementation : IRepository<Role, int>
    {
        UserRoledbCntext dbContext = new UserRoledbCntext();
        public void AddNew(Role entity)
        {
            dbContext.Roles.Add(entity);
            dbContext.SaveChanges();
        }

        public Role FindById(int id)
        {
            return dbContext.Roles.FirstOrDefault(c => c.IsActive == true && c.RoleId == id);
        }

        public IEnumerable<Role> GetAll()
        {
            return dbContext.Roles.Where(c => c.IsActive == true).ToList();
        }

        public void RemoveById(int id)
        {
            var role = FindById(id);
            dbContext.ChangeTracker.Clear();
            dbContext.ChangeTracker.DetectChanges();

            if (role is not null)
            {

                dbContext.Roles.Where(c => c.RoleId == id && c.IsActive == true)
                .ExecuteUpdate(setters =>
            setters.SetProperty(p => p.IsActive, false));
            }

        }

        public void Upsert(Role entity)
        {
            var role = FindById(entity.RoleId);
            dbContext.ChangeTracker.Clear();
            dbContext.ChangeTracker.DetectChanges();

            if (role != null)
            {
                dbContext.Roles.Where(c => c.IsActive == true && c.RoleId == entity.RoleId)
                .ExecuteUpdate(setters =>
            setters.SetProperty(p => p.RoleName, entity.RoleName)
            .SetProperty(p => p.RoleDescription, entity.RoleDescription));
            }
        }
    }
}
