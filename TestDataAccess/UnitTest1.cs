using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestDataAccess
{
    [TestClass]
    public class UnitTest1
    {
        IRepository<User, int> repo = new UserImplementation();
        IRepository<Role,int> repo2 = new RoleImplementation();
        [TestMethod]
        public void AddNew()
        {
            int expectedResult = repo.GetAll().Count() + 1;
            var user = new User();
            user.UserName = "Testdfasffsadfsd";
            user.Firstname = "Testadfdass";
            user.Lastname = "Testadfdsfadfdsfsasaf";
            user.Password = "Tesas123";
            user.IsActive = true;
            repo.AddNew(user);
            int actualResult = repo.GetAll().Count();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FindById()
        {
            User user = new User();
            user.UserName = "Testdfasdfsdasdfd";
            user.Firstname = "Testaddfddss";
            user.Lastname = "Testaddfdsdfdsfsasaf";
            user.Password = "Tesadd";
            user.IsActive = true;
            repo.AddNew(user);

            var result = repo.FindById(user.UserId);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.UserId, user.UserId);
        }
        [TestMethod]
        public void RemoveById()
        {
            int exp = repo.GetAll().Count();
            repo.RemoveById(26);
            int up = repo.GetAll().Count();
            Assert.AreEqual(exp - 1, up);
            
        }
        [TestMethod]
        public void GetAll()
        {
            int cnt = repo.GetAll().Count();
            User user = new User();
            user.UserName = "Testdfasdadfsdasdfd";
            user.Firstname = "Testaddfasdddss";
            user.Lastname = "Testaddfdsdfdfsadsfsasaf";
            user.Password = "Tesadaadd";
            user.IsActive = true;
            repo.AddNew(user);
            int cnt1 = repo.GetAll().Count();
            Assert.AreEqual(cnt + 1, cnt1);
        }
        [TestMethod]
        public void Update()
        {
            var user = repo.FindById(6);
            user.UserName = "Tani";
            repo.Upsert(user);
            var user2 = repo.FindById(6);
            Assert.AreEqual(user.UserName, user2.UserName);
        }

        [Ignore]
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithoutPasswordCols()
        {
            int expectedResult = repo.GetAll().Count() + 1;
            var user = new User();
            user.UserName = "Testdfasfdsadfsd";
            user.Firstname = "Testadfdsas";
            user.Lastname = "Testadfdsfdgsfsasaf";
            user.IsActive = true;
            repo.AddNew(user);
            int actualResult = repo.GetAll().Count();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UserNameLessThanFiveCharacters()
        {
            int expectedResult = repo.GetAll().Count() + 1;
            var user = new User();
            user.UserName = "Tes";
            user.Firstname = "Tesstadfdsas";
            user.Lastname = "Tesstadfdsfdgsfsasaf";
            user.Password = "TesftHello";
            user.IsActive = true;
            repo.AddNew(user);
            int actualResult = repo.GetAll().Count();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FindIdNotInDB()
        {
            User user = new User();
            user.UserName = "Royal";
            user.Firstname = "Challengers";
            user.Lastname = "Bangalore";
            user.Password = "RCB";
            user.IsActive = true;
            repo.AddNew(user);

            var result = repo.FindById(user.UserId);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(result.UserId+1, user.UserId);
        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))] 
        public void AddingToUserId()
        {
            User user = new User();
            user.UserId = 100; 
            user.UserName = "Chennai";
            user.Firstname = "Super";
            user.Lastname = "Kings";
            user.Password = "CSK";
            user.IsActive = true;
            repo.AddNew(user); 

            
            var result = repo.FindById(user.UserId);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(100, user.UserId);
        }
        [TestMethod]
        public void CheckForRoleId_Throws_NullReferenceException()
        {
            int id = 100;
            var repo1 = new RoleImplementation();
            repo1.FindById(id);
        }

    }

}