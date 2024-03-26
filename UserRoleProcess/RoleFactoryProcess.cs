using CaseStudy1.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleProcess
{
    public class RoleFactoryProcess
    {
        
        public static Role CreateNewAccount(string RoleName, string RoleDescription,bool IsActive)
        {
            if (RoleName.Length >= 5)
            {
                Role role = new Role();
                role.RoleDescription = RoleDescription;
                role.IsActive = IsActive;
                role.RoleName = RoleName;
                return role;
            }
            return null;
        }

    }
}
