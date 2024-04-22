using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using Microsoft.Extensions.Options;

namespace CaseStudyMVC.Infrastructure
{
    public class RoleApiRepository : IRepositoryAsync<Role>
    {

        private readonly ApiConfigurations _apiConfig;
        private readonly Role role;
        private readonly string token;
        public RoleApiRepository(
            IHttpContextAccessor contextAccessor,
            IOptions<ApiConfigurations> options)
        {
            _apiConfig = options.Value;
            token = contextAccessor.HttpContext.Session.GetString("Token")!;
            var userString = contextAccessor.HttpContext.Session.GetString("User")!;
            role = ConvertData.JsonStringToObject<Role>(userString)!;
        }
        public async Task CreateNew(Role entity)
        {
            var result = await ApiHelper.ExecuteHttpPost<Role, Role>(
                url: $"{_apiConfig.RoleUrl}/createnew",
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!,
                inputObj: entity);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            var result = await ApiHelper.ExecuteHttpGet<List<Role>>(
                url: _apiConfig.RoleUrl!,
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!);
            return result;
        }

        public async Task<Role> GetById(int id)
        {
            var result = await ApiHelper.ExecuteHttpGet<Role>(
                url: $"{_apiConfig.RoleUrl}/{id}",
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!);
            return result;
        }

        public async Task Remove(int id)
        {
            var result = await ApiHelper.ExecuteHttpDelete<Role, Role>(
                url: $"{_apiConfig.RoleUrl}/delete/{id}",
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!);
        }

        public async Task Update(Role entity)
        {
            var result = await ApiHelper.ExecuteHttpPut<Role, Role>(
                url: $"{_apiConfig.RoleUrl}/update/{entity.RoleId}",
                token: token,
                baseUrl: _apiConfig.RoleBaseUrl!,
                inputObj: entity);
        }
    }
}
