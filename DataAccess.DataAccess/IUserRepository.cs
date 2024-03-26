using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccess
{
    public interface IUserRepository : IRepository<User, int>
    {
        int GetRoleIdForUser(int userId);
        void UpdateUserRole(UserRole entity);
    }
}
