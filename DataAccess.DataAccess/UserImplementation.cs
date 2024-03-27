using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy1.DataAccess.Repositories
{
    public class UserImplementation : IUserRepository
    {
        UserRoledbCntext dbContext = new UserRoledbCntext();
        public void AddNew(User entity)
        {
           dbContext.Users.Add(entity);
           dbContext.SaveChanges();
        }

        public User FindById(int id)
        {
            var mes = dbContext.Users.FirstOrDefault(c => c.IsActive == true && c.UserId == id);

            if (mes != null)
            {
                return mes;
            }
            else
            {
                throw new Exception("No");
            }
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Users.Where(c => c.IsActive==true).ToList();
        }

        public int GetRoleIdForUser(int userId)
        {
            return dbContext.UserRoles.Where(c => c.UserId == userId).Select(c => c.RoleId).FirstOrDefault();
        }

        public void RemoveById(int id)
        {
            var usr = FindById(id);
            dbContext.ChangeTracker.Clear();
            dbContext.ChangeTracker.DetectChanges();

            if (usr is not null)
            {

                dbContext.Users.Where(c => c.UserId == id && c.IsActive == true)
                .ExecuteUpdate(setters =>
            setters.SetProperty(p => p.IsActive,false));
            }
            
            //dbContext.SaveChanges();

        }

        public void UpdateUserRole(UserRole entity)
        {
            var ent = dbContext.UserRoles.Where(c=> c.UserId == entity.UserId).FirstOrDefault();
            if (ent is null)
            {
                dbContext.UserRoles.Add(entity);
                dbContext.SaveChanges();
            }
            else
            {
                ent.RoleId = entity.RoleId;
            }
        }

        public void Upsert(User entity)
        {
            var usr = FindById(entity.UserId);
            dbContext.ChangeTracker.Clear();
            dbContext.ChangeTracker.DetectChanges();

            if (usr != null)
            {
                dbContext.Users.Where(c => c.IsActive == true && c.UserId == entity.UserId)
                    .ExecuteUpdate(setters =>
                setters.SetProperty(p => p.UserName, entity.UserName)
                .SetProperty(p => p.Password, entity.Password)
                .SetProperty(p => p.Firstname, entity.Firstname)
                .SetProperty(p => p.Lastname, entity.Lastname));

                
            }
            //dbContext.SaveChanges();
        }    
    }
}
