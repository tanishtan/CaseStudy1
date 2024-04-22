using CaseStudy1.DataAccess;
using WebClassLibrary;

namespace AuthenticationAPI.Infrastructure
{
    public interface IUserServiceAsync
    {
        Task<User> AuthenticateAsync(AuthenticationRequest model);
        Task<User> GetUserDetails(int userId);
    }
    public class UserService : IUserServiceAsync
    {
        private readonly AuthDbContext _context;
        public UserService(AuthDbContext context)
        {
            _context = context;
        }
        public Task<User> AuthenticateAsync(AuthenticationRequest model)
        {
            var user = _context.Users.Where(c => c.UserName == model.Username && c.Password == model.Password).FirstOrDefault();
            //var user = _context.FirstOrDefault(c => c.Username == model.Username && c.Password == model.Password);
            return Task.Run(() => user);
        }
        public Task<User> GetUserDetails(int userId)
        {
            var user = _context.Users.Where(c => c.UserId == userId).FirstOrDefault();
            //var user = _context.FirstOrDefault(c => c.UserId == userId);
            return Task.Run(() => user);
        }
    }
}
