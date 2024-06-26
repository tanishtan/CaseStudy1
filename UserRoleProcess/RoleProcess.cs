﻿using CaseStudy1.DataAccess.Repositories;
using CaseStudy1.DataAccess;
using DataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRoleProcess
{
    public class RoleProcess
    {
        IRepository<Role, int> RoleRepo = new RoleImplementation();
        public Role[] GetAllRoles()
        {
            return RoleRepo.GetAll().ToArray();
        }
        public void CreateNewRole(string RoleName, string RoleDescription, bool IsActive)
        {
            RoleRepo.AddNew(RoleFactoryProcess.CreateNewAccount(RoleName, RoleDescription, IsActive));

        }
        public Role FindByIdRole(int Id)
        {
            try
            {
                var id = RoleRepo.FindById(Id);
                return id;
            }
            catch(NullReferenceException ex) 
            {
                throw ex;
            }
        }
        public void RemoveByIdRole(int Id)
        {
            try
            {
                RoleRepo.RemoveById(Id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        public void UpdateRole(int id, string RoleName, string RoleDescription, bool IsActive)
        {
            var user = RoleFactoryProcess.CreateNewAccount(RoleName, RoleDescription, IsActive);
            user.RoleId = id;
            RoleRepo.Upsert(user);

        }
    }
}
