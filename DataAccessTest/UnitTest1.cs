using CaseStudy1.DataAccess;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessTest
{
    [TestClass]
    public class UnitTest1
    {
        IRepostitory<User, int> repostitory = new UserImplementation();
        [TestMethod]
        public void AddNewUser()
        {
            int expectedResutlt = repostitory.GetAll().Count() + 1;
        }
    }
}