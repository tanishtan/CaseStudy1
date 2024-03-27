using CaseStudy1.DataAccess;
using DataAccess.DataAccess;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Update.Internal;
using UserRoleProcess;

namespace CaseStudy1
{
    internal class Program
    {
        static UserProcess process = new UserProcess();
        static RoleProcess roleProcess = new RoleProcess();
        static int DisplayMenuAndGetChoice()
        {
            //Console.Clear();
            Console.WriteLine("************** Login Management System ***********");
            Console.WriteLine("******* 1. Manage Users ");
            Console.WriteLine("******* 2. Manage Roles");
            Console.WriteLine("******* 3. Manage User Roles");
            Console.WriteLine("******* ");
            Console.WriteLine("******* 0. Quit ");
            Console.WriteLine("***********************************");
            Console.Write("\nEnter Choice: ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice >= 0 && choice < 4) return choice;
            }
            else
            {
                Console.WriteLine("Invalid Choice.");
            }
            return choice;
        }

        static void ManagingUser()
        {
            Console.Clear();
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("************** Login Management System ***********");
                Console.WriteLine("************** Manage Users ***********");
                Console.WriteLine("******* 1. List All Users ");
                Console.WriteLine("******* 2. Find User By Id");
                Console.WriteLine("******* 3. Add New User");
                Console.WriteLine("******* 4. Update User Details");
                Console.WriteLine("******* 5. Remove User");
                Console.WriteLine("******* ");
                Console.WriteLine("******* 0. Back To Main Menu ");
                Console.WriteLine("***********************************");
                Console.Write("\nEnter Choice: ");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    var users = process.GetAllUser();
                    PrintUser(users);
                    Console.ReadKey();
                }
                else if(choice==2)
                {

                    int id = 0;
                    Console.WriteLine("Enter the id to be found");
                    try
                    {
                        int.TryParse(Console.ReadLine(),out id);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("Enter a valid number");
                        Console.ReadKey();
                        ManagingUser();
                    }
                    FindUserById(id);
                    Console.ReadKey();
                }
                else if(choice==3)
                {
                    
                    Console.WriteLine("Enter the UserName");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter the FirstName");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("Enter the LastName");
                    string lastname = Console.ReadLine();
                    Console.WriteLine("Enter the Password");
                    string password = Console.ReadLine();
                    AddingNewUser(username, firstname, lastname, password);
                    Console.ReadKey();
                }
                else if(choice==4)
                {
                    
                    Console.WriteLine("Enter the user details to be updated");
                    Console.WriteLine("Enter the Id");
                    int id = 0;
                    try
                    {
                        id = int.Parse(Console.ReadLine());
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                        Console.WriteLine("Enter a number");
                        ManagingRole();
                    }
                    try
                    {
                        var user = process.FindByIdUser(id);
                    if (user == null)
                    {
                        Console.WriteLine("Id not found");
                        ManagingUser();
                    }
                    
                    Console.WriteLine("Enter the UserName");
                    string username = Console.ReadLine();
                    if (username.Length == 0)
                        username = user.UserName;
                    Console.WriteLine("Enter the FirstName");
                    string firstname = Console.ReadLine();
                    if (firstname.Length == 0)
                        firstname = user.Firstname;
                    Console.WriteLine("Enter the LastName");
                    string lastname = Console.ReadLine();
                    if (lastname.Length == 0)
                        username = user.Lastname;
                    Console.WriteLine("Enter the Password");
                    string password = Console.ReadLine();
                    if (password.Length == 0)
                        password = user.Password;
                    
                        UpdateUser(id, username, firstname, lastname, password);
                    }
                    catch( Exception ex )
                    {
                        Console.WriteLine("User cannot be updated");
                    }
                    Console.ReadKey();
                }
                else if(choice==5)
                {
                    int id = 0;
                    Console.WriteLine("Enter the id to be removed");
                    try
                    {
                        id = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Id not found");
                        ManagingUser();
                    }
                    RemoveUserId(id);
                    Console.ReadKey();
                }
            } while (choice != 0);
            Console.Clear();
            DisplayMenuAndGetChoice();
        }

        public static void PrintUser(User[] users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"UserId: {user.UserId}");
                Console.WriteLine($"UserName: {user.UserName}");
                Console.WriteLine($"FirstName: {user.Firstname}");
                Console.WriteLine($"LastName: {user.Lastname}");
            }
        }

        public static void FindUserById(int id)
        {
            try
            {
                var user = process.FindByIdUser(id);
                if(user == null)
                {
                    Console.WriteLine("Id not found");
                    ManagingUser();
                }
                Console.WriteLine($"UserId: {user.UserId}");
                Console.WriteLine($"UserName: {user.UserName}");
                Console.WriteLine($"FirstName: {user.Firstname}");
                Console.WriteLine($"LastName: {user.Lastname}");
                Console.WriteLine();
                Console.ReadKey(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("You did not enter a number");
                
            }
            
        }

        public static void AddingNewUser(string username,string firstname,string lastname, string password)
        {
            try
            {
                process.CreateNewUser(username, firstname, lastname, password, true);
                Console.WriteLine("User Created");
            }
            catch(NullReferenceException ex) 
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Some credentials are empty");
            }
        }


        public static void UpdateUser(int id, string username, string firstname, string lastname, string password)
        {
            try
            {
                process.UpdateUser(id, username, firstname, lastname, password, true);
                Console.WriteLine("Updated");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Give all valid credentials");
            }
        }

        public static void RemoveUserId(int id)
        {
            try
            {
                process.RemoveByIdUser(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            Console.WriteLine("The Id is deleted");
        }
        static void ManagingRole()
        {
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("************** Login Management System ***********");
                Console.WriteLine("************** Manage Users ***********");
                Console.WriteLine("******* 1. List All Roles ");
                Console.WriteLine("******* 2. Find Role By Id");
                Console.WriteLine("******* 3. Add New Role");
                Console.WriteLine("******* 4. Update Role Details");
                Console.WriteLine("******* 5. Remove Role");
                Console.WriteLine("******* ");
                Console.WriteLine("******* 0. Back To Main Menu ");
                Console.WriteLine("***********************************");
                Console.Write("\nEnter Choice: ");
                choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    var roles = roleProcess.GetAllRoles();
                    PrintRole(roles);
                    Console.ReadKey();
                }
                else if (choice == 2)
                {

                    Console.WriteLine("Enter the id to be found");
                    int id = int.Parse(Console.ReadLine());
                    FindRoleById(id);
                    Console.ReadKey();
                }
                else if (choice == 3)
                {

                    Console.WriteLine("Enter the RoleName");
                    string roleName = Console.ReadLine();
                    Console.WriteLine("Enter the Description");
                    string roleDescription = Console.ReadLine();
                    AddingNewRole(roleName,roleDescription);
                    Console.ReadKey();
                }
                else if (choice == 4)
                {

                    Console.WriteLine("Enter the role details to be updated");
                    Console.WriteLine("Enter the Id");
                    int id=0;
                    try
                    {
                        id = int.Parse(Console.ReadLine());
                    }
                    catch(FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("The Id must be an integer");
                        Console.ReadKey();
                        ManagingRole();
                    }
                    var role = roleProcess.FindByIdRole(id);
                    Console.WriteLine("Enter the RoleName");
                    string roleName = Console.ReadLine();
                    if (roleName.Length == 0)
                        roleName = role.RoleName;
                    Console.WriteLine("Enter the RoleDescription");
                    string roleDesc = Console.ReadLine();
                    if (roleDesc.Length == 0)
                        roleDesc = role.RoleDescription;
                    
                    UpdateRole(id, roleName, roleDesc);
                    Console.ReadKey();
                }
                else if (choice == 5)
                {
                    try
                    {
                        Console.WriteLine("Enter the id to be removed");
                        int id = int.Parse(Console.ReadLine());
                        RemoveRoleId(id);
                        Console.ReadKey();
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                    }
                }
                
            } while (choice != 0);
            Console.Clear();
            DisplayMenuAndGetChoice();
        }
        public static void PrintRole(Role[] roles)
        {
            foreach (var role in roles)
            {
                Console.WriteLine($"RoleId: {role.RoleId}");
                Console.WriteLine($"RoleName: {role.RoleName}");
                Console.WriteLine($"RoleDescription: {role.RoleDescription}");
            }
        }

        public static void FindRoleById(int id)
        {
            try
            {
                var role = roleProcess.FindByIdRole(id);
                Console.WriteLine($"RoleId: {role.RoleId}");
                Console.WriteLine($"RoleName: {role.RoleName}");
                Console.WriteLine($"RoleDescription: {role.RoleDescription}");
                Console.WriteLine();
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Role with ID {id} not found.");
                
            }
        }

        public static void AddingNewRole(string roleName, string roleDescrition)
        {
            try
            {
                roleProcess.CreateNewRole(roleName, roleDescrition, true);
                Console.WriteLine("Role Created");
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"The RoleName is a required string");
            }
            
        }


        public static void UpdateRole(int id, string roleName, string roleDescrition)
        {
            try
            {
                roleProcess.UpdateRole(id, roleName, roleDescrition, true);
                Console.WriteLine("Updated");
            }
            catch(FormatException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine($"The RoleId must be an integer");
            }
            catch(NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RemoveRoleId(int id)
        {
            roleProcess.RemoveByIdRole(id);

            Console.WriteLine("The Id is deleted");
        }

        static void ManagingUserRole()
        {
            int choice = 0;
            
            Console.Clear();
            Console.WriteLine("************** Login Management System ***********");
            Console.WriteLine("************** Manage User Role ***********");
            Console.WriteLine("Enter User Id ");
            try
            {
                choice = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Enter a valid number");
                Console.ReadKey();
                ManagingUserRole();
            }
            Console.WriteLine("Roles Available");
            RoleImplementation rp = new RoleImplementation();
            var all = rp.GetAll(); 
            foreach ( var item in all)
            {
                Console.Write($"{item.RoleName}   ");
            }
            Console.WriteLine();
            try
            {
                Console.WriteLine("Enter Role Name");

                string roleName = Console.ReadLine();

                Console.WriteLine("Save this mapping Y/N");
                string check = Console.ReadLine();
                int id = 0;
                if (check.ToLower() == "y")
                {
                    foreach (var item in all)
                    {
                        if (item.RoleName == roleName)
                        {
                            id = item.RoleId;
                        }
                    }

                    process.UpdateRole(choice, id);
                }
                else
                {
                    Console.Clear();
                    DisplayMenuAndGetChoice();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            
        }

        static void Main(string[] args)
        {
            int choice = 0;
            do
            {
                Console.Clear();
                choice = DisplayMenuAndGetChoice();
                switch(choice)
                {
                    
                    case 1:
                        ManagingUser();
                        break;
                    case 2: 
                        ManagingRole();
                        break;
                    case 3:
                        ManagingUserRole();
                        break;


                    

                }
            }while(choice != 0);
        }
    }
}
