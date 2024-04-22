using CaseStudy1.DataAccess.Repositories;
using CaseStudy1.DataAccess;
using CaseStudyMVC.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CaseStudyMVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRepositoryAsync<Role> _repositoryAsync;
        private readonly ApiConfigurations _apiConfig;
        public RoleController(
            IRepositoryAsync<Role> repositoryAsync,
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
        public async Task<IActionResult> Create(Role role)
        {
            await _repositoryAsync.CreateNew(role);
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
        public async Task<IActionResult> Edit(Role role)
        {
            await _repositoryAsync.Update(role);
            return RedirectToAction("List");
        }
    }
}
