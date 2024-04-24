using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using CaseStudyMVC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Core.Types;
using UserRoleProcess;

namespace CaseStudyMVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepositoryAsync<User> _repositoryAsync;
        private readonly ApiConfigurations _apiConfig;
        public UserController(
            IRepositoryAsync<User> repositoryAsync,
            IOptions<ApiConfigurations> options)
        {
            _repositoryAsync = repositoryAsync;
            _apiConfig = options.Value;
        }

        [TokenCheck]
        public async Task<IActionResult> List()
        {
            var model = await _repositoryAsync.GetAll();
            return View(model);
        }

        [TokenCheck]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _repositoryAsync.GetById(id);
            return View(model);
        }

        [HttpGet]
        [TokenCheck]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [TokenCheck]
        public async Task<IActionResult> Create(User user)
        {
            await _repositoryAsync.CreateNew(user);
            return RedirectToAction("List"); 
        }

        [HttpGet]
        [TokenCheck]
        public async Task<IActionResult> Remove(int id)
        {
            var model = await _repositoryAsync.GetById(id);
            if (model == null)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }

        [ActionName("Remove")]
        [HttpPost]
        [TokenCheck]
        public async Task<IActionResult> RemoveConfirmed(int id)
        {
            await _repositoryAsync.Remove(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        [TokenCheck]
        public async Task<IActionResult> Edit()
        {
            return View();
        }

        //[HttpPut]
        [TokenCheck]
        public async Task<IActionResult> Edit(User user)
        {
            await _repositoryAsync.Update(user);
            return RedirectToAction("List");
        }

        public IActionResult MapUserToRole(UserRole userRole)
        {
            UserProcess user = new UserProcess();
            user.MapUserRole(userRole);
            return View(user);
        }

    }
}
