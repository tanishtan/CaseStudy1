using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using Microsoft.Extensions.Options;

namespace CaseStudyMVC.Infrastructure
{
    public class UserApiRepository : IRepositoryAsync<User>
    {
        private readonly ApiConfigurations _apiConfig;
        private readonly User user;
        private readonly string token;
        public UserApiRepository(
            IHttpContextAccessor contextAccessor,
            IOptions<ApiConfigurations> options)
        {
            _apiConfig = options.Value;
            token = contextAccessor.HttpContext.Session.GetString("Token")!;
            var userString = contextAccessor.HttpContext.Session.GetString("User")!;
            user = ConvertData.JsonStringToObject<User>(userString)!;
        }
        public async Task CreateNew(User entity)
        {
            var result = await ApiHelper.ExecuteHttpPost<User, User>(
                url: $"{_apiConfig.UserUrl}/createnew",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!,
                inputObj: entity);
            
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var result = await ApiHelper.ExecuteHttpGet<List<User>>(
                url: _apiConfig.UserUrl!,
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!);
            return result;
        }

        public async Task<User> GetById(int id)
        {
            var result = await ApiHelper.ExecuteHttpGet<User>(
                url: $"{_apiConfig.UserUrl}/{id}",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!);
            return result;
        }

        public async Task Remove(int id)
        {
            var result = await ApiHelper.ExecuteHttpDelete<User, User>(
                url: $"{_apiConfig.UserUrl}/delete/{id}",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!);
        }

        public async Task Update(User entity)
        {
            var result = await ApiHelper.ExecuteHttpPut<User, User>(
                url: $"{_apiConfig.UserUrl}/update/{entity.UserId}",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!,
                inputObj: entity);
        }

        /*public UserRole MapUserRole(int userId)
        {
            var result = ApiHelper.ExecuteHttpPost<User, User>(
                url: $"{_apiConfig.UserUrl}/mapUserToRoles",
                token: token,
                baseUrl: _apiConfig.UserBaseUrl!,
                inputObj: userId);
        }*/
    }
}
