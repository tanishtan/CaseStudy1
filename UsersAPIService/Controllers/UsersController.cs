using CaseStudy1.DataAccess;
using CaseStudy1.DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserRoleProcess;

namespace UsersAPIService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserProcess _userProcess;
        
        public UsersController(UserProcess userProcess)
        {
            _userProcess = userProcess;
        }
        [HttpGet(template: "")]
        public IActionResult GetAllUsers()
        {
            var model = _userProcess.GetAllUser();
            return Ok(model);
        }
        [HttpGet(template: "{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var model = _userProcess.FindByIdUser(id);
            if (model is not null)
            {
                return model;
            }
            else
                return NotFound();
        }

        [HttpPost(template: "createnew")]
        public IActionResult CreateNewUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("Please provide role information in the request body");
            }

            _userProcess.CreateNewUser(user.UserName, user.Firstname, user.Lastname, user.Password, true);
            return Ok(user);

        }

        [HttpPut(template: "update/{id}")]
        public IActionResult UpdateRole(int id, [FromBody] User user)
        {
            if (user == null || id != user.UserId)
            {
                return BadRequest("Invalid user information or mismatch between URL ID and request body ID");
            }

            _userProcess.UpdateUser(id, user.UserName, user.Firstname, user.Lastname, user.Password, true);
            var updateRole = _userProcess.FindByIdUser(id);
            return Ok(updateRole);

        }

        [HttpDelete(template: "delete/{id}")]
        public IActionResult DeleteRole(int id)
        {
            var model = _userProcess.FindByIdUser(id);
            _userProcess.RemoveByIdUser(id);
            
            //return model;
            return Ok(model);

        }

        [HttpPost(template: "mapUserToRoles")]
        public IActionResult MapUserToRoles([FromBody] UserRole mapping)
        {
            if (mapping == null || mapping.UserId <= 0 || mapping.RoleId == null)
            {
                return BadRequest("Invalid user ID, role IDs, or missing data in request body");
            }

            _userProcess.MapUserRole(mapping);
            var mappedUser = _userProcess.GetAllUser();
            return Ok(mappedUser);
            
        }
    }
}
