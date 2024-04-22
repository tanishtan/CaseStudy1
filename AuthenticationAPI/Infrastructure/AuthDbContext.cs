using CaseStudy1.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AuthenticationAPI.Infrastructure
{
    public class AuthDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
    }
}
