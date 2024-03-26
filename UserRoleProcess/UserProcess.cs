using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleProcess
{
    public class UserProcess
    {
        IRepository<User, int> UserRepo = new UserImplementation();
        IUserRepository repo = new UserImplementation();
        
        public User[] GetAllUser()
        {
            return UserRepo.GetAll().ToArray();
        }
        public void CreateNewUser(string UserName, string FirstName, string LastName, string Password, bool IsActive)
        {
            UserRepo.AddNew(UserFactoryProcess.CreateNewAccount(UserName, FirstName, LastName, Password, IsActive));
            
        }
        public User FindByIdUser(int Id)
        {
            var id = UserRepo.FindById(Id);
            return id;
        }
        public void RemoveByIdUser(int Id) 
        {
            var user = FindByIdUser(Id);
            if (user != null)
            {
                UserRepo.RemoveById(Id);
            }
            else
            {
                Console.WriteLine("User does not exist");
            }
        }
        public void UpdateUser(int id, string UserName, string FirstName, string LastName, string Password, bool IsActive)
        {
            var user = UserFactoryProcess.CreateNewAccount(UserName, FirstName, LastName, Password, IsActive);
            user.UserId = id;
            UserRepo.Upsert(user);
        }

        public Role GetRoleForUser(int id)
        {
            var roleId = repo.GetRoleIdForUser(id);
            RoleImplementation rep = new RoleImplementation();
            var ent = rep.FindById(roleId);
            return ent;
        }
        public void UpdateRole(int userId, int roleId)
        {
            UserRole rp = new UserRole();
            rp.UserId = userId;
            rp.RoleId = roleId;
            rp.IsActive = true;
            repo.UpdateUserRole(rp);
        }
    }
}
