using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataAccess
{
    [TestClass]
    public class UnitTest2
    {
        
        IUserRepository repo = new UserImplementation();
        [TestMethod]
        public void TestForUpdate()
        {
            UserRole userRole = new UserRole();
            userRole.UserId = 1;
            userRole.RoleId = 2;
            userRole.IsActive = true;
            repo.UpdateUserRole(userRole);
            int id = repo.GetRoleIdForUser(userRole.UserId);
            //Assert.IsNotNull(newusr);
            //Assert.AreEqual(userRole.IsActive, newusr.IsActive);
            Assert.AreEqual(userRole.RoleId, id);
        }
    }
}
