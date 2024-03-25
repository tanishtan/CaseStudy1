using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy1.DataAccess
{     
    [Table("Users")]
    [PrimaryKey("UserId")]
    [Index("UserName", IsUnique = true, Name = "IDX_UserName")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public string UserName { get; set; }
        [Required]
        [StringLength(50)]
        [MinLength(8)]
        public string Password { get; set; }
        [StringLength(50)]
        public string Firstname { get; set; }
        [StringLength(50)]
        public string Lastname { get; set; }
        public bool IsActive { get; set; } = true;

    }

    [Table("Roles")]
    [PrimaryKey("RoleId")]
    [Index("RoleName", IsUnique = true, Name = "IDX_RoleName")]
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        [StringLength(50)]
        [Required]
        [MinLength(5)]
        public string RoleName { get; set; }
        [StringLength(50)]
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; } = true;
    }

    [Table("UserRoles")]
    [PrimaryKey("UserId","RoleId")]
    public class UserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("UserId")]
        public User User1 { get; set; }

        [ForeignKey("RoleId")]
        public Role Role1 { get; set; }  

    }

    public class UserRoledbCntext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"server=(local);database=CaseStudy1;integrated security=sspi;trustservercertificate=true"
                );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
          /*  //modelBuilder.Entity<Book>().HasKey(c => c.BookId);
            modelBuilder.Entity<Book>().HasIndex(c => c.ISBN);
            modelBuilder.Entity<Book>().HasData(
                 new Book { BookId = 1, Title = "EF Core", Author = "Eurofins", Price = 1000m, ISBN = "12345", IsActive = true, PublicationDate = new DateTime(2023, 12, 12) },
                 new Book { BookId = 2, Title = "EF Core Tools", Author = "Eurofins", Price = 1100m, ISBN = "1234598765", IsActive = false, PublicationDate = new DateTime(2023, 12, 12) }
                );*/
        }
    }
}
