using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRoleProcess;

namespace RolesAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleProcess _roleProcess;

        public RolesController(RoleProcess roleProcess)
        {
            _roleProcess = roleProcess;
        }

        [HttpGet(template: "")]
        public IActionResult GetAllRoles()
        {
            var model = _roleProcess.GetAllRoles();
            return Ok(model);
        }
        [HttpGet(template: "{id}")]
        public ActionResult<Role> GetRole(int id)
        {
            var model = _roleProcess.FindByIdRole(id);
            if (model is not null)
            {
                return model;
            }
            else
                return NotFound();
        }

        [HttpPost(template: "createnew")]
        public IActionResult CreateNewRole([FromBody] Role role)
        {
            if (role == null)
            {
                return BadRequest("Please provide role information in the request body");
            }

            _roleProcess.CreateNewRole(role.RoleName, role.RoleDescription, true);
            return Ok(role);    

        }

        [HttpPut(template: "update/{id}")]
        public IActionResult UpdateRole(int id, [FromBody] Role role)
        {
            if (role == null || id != role.RoleId)
            {
                return BadRequest("Invalid user information or mismatch between URL ID and request body ID");
            }

            _roleProcess.UpdateRole(id, role.RoleName, role.RoleDescription, true);
            var updateRole = _roleProcess.FindByIdRole(id);
            return Ok(updateRole);
            
        }

        [HttpDelete(template: "delete/{id}")]
        public IActionResult DeleteRole(int id)
        {
            var model = _roleProcess.FindByIdRole(id);
            _roleProcess.RemoveByIdRole(id);
            return Ok(model);
         
        }
    }
}
