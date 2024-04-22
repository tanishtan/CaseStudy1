using AuthenticationAPI.Infrastructure;
using CaseStudy1.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebClassLibrary;

namespace AuthenticationAPI.Controllers
{
    
    //[Route("api/[controller]")]
    [Route("")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        /*private readonly AuthDbContext _context;
        public AccountsController(AuthDbContext context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]
        //[HttpPost(template: "authenticate/{username}/{password}")]
        public IActionResult Authenticate(string username, string password)
        {
            var item = _context.Users.FirstOrDefault(User => User.UserName == username && User.Password == password);
            return Ok(item);
        }

        [HttpGet("validate")]
        public IActionResult Validate()
        {
            if (string.IsNullOrEmpty(Request.Headers["Authorization"]))
            {
                return BadRequest("Authorization header is missing");
            }
            if (Request.Body.Length == 0)
            {
                return BadRequest("Request body is empty");
            }
            return Ok(true);
        }*/

        private readonly AppSettings _settings;
        private readonly IUserServiceAsync _userService;

        public AccountsController(
            IOptions<AppSettings> options,
            IUserServiceAsync service
        )
        {
            _settings = options.Value;
            _userService = service;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthenticationResponse>> Login(AuthenticationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.AuthenticateAsync(model);
            if (user is null)
            {
                return NotFound("Bad Username/password.");
            }
            var token = TokenManager.GenerateWebToken(user, _settings);
            var authResponse = new AuthenticationResponse(user, token);

            return authResponse;
        }

        //URL: api/accounts/validate 
        [HttpGet(template: "validate")]
        public async Task<ActionResult<User>> Validate()
        {
            var user = HttpContext.Items["User"] as User;
            if (user is null)
            {
                return Unauthorized("You are not authorized to access this application.");
            }
            return user;
        }

    }
}
