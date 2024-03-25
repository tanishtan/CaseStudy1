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
    public class UserRoleImplementation : IRepository<UserRole, int>
    {
        public void AddNew(UserRole entity)
        {
            throw new NotImplementedException();
        }

        public UserRole FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public void RemoveById(int id)
        {
            throw new NotImplementedException();
        }

        public void Upsert(UserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
