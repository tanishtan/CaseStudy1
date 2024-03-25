using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy1.DataAccess.Repositories
{
    public class UserImplementation : IRepository<User, int>
    {
        UserRoledbCntext dbContext = new UserRoledbCntext();
        public void AddNew(User entity)
        {
           dbContext.Users.Add(entity);
           dbContext.SaveChanges();
        }

        public User FindById(int id)
        {
            return dbContext.Users.FirstOrDefault(c => c.IsActive==true && c.UserId == id);
        }

        public IEnumerable<User> GetAll()
        {
            return dbContext.Users.Where(c => c.IsActive==true).ToList();
        }

        public void RemoveById(int id)
        {
            
            
            dbContext.Users.Where(c => c.UserId == id && c.IsActive==true)
                .ExecuteUpdate(setters =>
            setters.SetProperty(p => p.IsActive, false));
            
            

        }

        public void Upsert(User entity)
        {
            var usr = FindById(entity.UserId);
            

            
            dbContext.Users.Where(c =>c.IsActive==true && c.UserId == entity.UserId)
                .ExecuteUpdate(setters =>
            setters.SetProperty(p => p.UserName, entity.UserName)
            .SetProperty(p => p.Password, entity.Password)
            .SetProperty(p => p.Firstname, entity.Firstname)
            .SetProperty(p => p.Lastname, entity.Lastname));
            
            dbContext.SaveChanges();
        }
    }
}
