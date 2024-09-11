using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TestAuthentificationToken.Services;

namespace TestAuthentificationToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Admins")]
        [Authorize]
        public IActionResult AdminsEndpoint()
        {
            var currentUser = _userService.GetCurrentUser();
            if (currentUser != null) {
                return Ok($"Hi {currentUser.GivenName}, you are an {currentUser.Role}");
            } else {
                return Unauthorized("You need to be Administrator !");
            }
        }

        [HttpGet("Sellers")]
        [Authorize(Roles = "Seller")]
        public IActionResult SellersEndpoint()
        {
            var currentUser = _userService.GetCurrentUser();
            if (currentUser != null) {
                return Ok($"Hi {currentUser.GivenName}, you are a {currentUser.Role}");
            } else {
                return Unauthorized("You need to be Seller !");
            }
        }

        [HttpGet("AdminsAndSellers")]
        [Authorize(Roles = "Administrator,Seller")]
        public IActionResult AdminsAndSellersEndpoint()
        {
            var currentUser = _userService.GetCurrentUser();
            if (currentUser != null) {
                return Ok($"Hi {currentUser.GivenName}, you are an {currentUser.Role}");
            } else {
                return Unauthorized("You need to be Administrator or Seller !");
            }
        }

        [HttpGet("Public")]
        public IActionResult Public()
        {
            return Ok("Hi, you're on public property");
        }
    }
}
