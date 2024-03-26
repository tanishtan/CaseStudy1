using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UserRoleProcess
{
    public class UserFactoryProcess
    {
        
        
        public static User CreateNewAccount(string UserName, string FirstName, string LastName, string Password,bool IsActive)
        {
            
            if (Password.Length >= 8 && Regex.IsMatch(Password, @"[0-9]") && UserName.Length >= 5)
            {
                User user = new User();
                user.UserName = UserName;
                user.Firstname = FirstName;
                user.Lastname = LastName;
                user.Password = Password;
                user.IsActive = IsActive;
                return user;
            }
            return null;
        }
    }
}
